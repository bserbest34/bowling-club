using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VIPNavMesh : MonoBehaviour
{
    public AreaType areaType;
    private NavMeshAgent navMeshAgent;

    private Transform exit;
    public GameObject currentArea;
    internal bool isPlayable = true;
    private int playTime = 0;
    private GameObject AIBall;
    public Transform pickBallArea;
    public Transform throwBallArea;

    private Animator anim;
    bool isOnPlayBowling = false;
    bool waitForPlay = false;
    public NavMeshAgent bodyguard1, bodyguard2;
    Vector3 beginPos;
    Quaternion beginRot;
    internal bool isPlayeable = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playTime = 0;
        if (transform.Find("ShelfUpgradeOverlayCanvas") != null)
            transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
        transform.position = beginPos;
        transform.rotation = beginRot;
    }

    private void Awake()
    {
        beginPos = transform.position;
        beginRot = transform.rotation;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (transform.Find("Armature") != null)
        {
            AIBall = transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("mixamorig:RightHandMiddle2").Find("mixamorig:RightHandMiddle3").Find("Ball").gameObject;

        }
        else
        {
            AIBall = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("mixamorig:RightHandMiddle2").Find("mixamorig:RightHandMiddle3").Find("Ball").gameObject;
        }

        exit = GameObject.Find("CustomerExit").transform;
    }

    private void Update()
    {
        anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        if (!isPlayable)
            return;
        Bowling();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tags.Exit:
                enabled = false;
                gameObject.SetActive(false);
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("VipPoint1"))
        {
            transform.DOLookAt(throwBallArea.transform.position, 0.2f);
        }

        if(other.CompareTag("WaitPoint"))
        {
            transform.DOLookAt(throwBallArea.transform.position, 0.2f);
        }
        if(other.CompareTag(Tags.BowlingAreaTarget))
        {
            if (currentArea == null)
                return;
            transform.DOLookAt(currentArea.transform.Find("Lookat").transform.position, 0.5f);
            if (!isOnPlayBowling && AIBall.activeInHierarchy)
                StartCoroutine(AIPlayBowling());
        }
        if(other.CompareTag(Tags.BallPlatform))
        {
            AIPickBall(other);
        }
    }

    void AIPickBall(Collider other)
    {
        if (currentArea == null)
            return;
        if (waitForPlay)
            return;
        if (currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.Count <= 0)
            return;
        waitForPlay = true;
        float z = 0;
        AIBall.GetComponent<MeshRenderer>().materials =
            currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls[0].GetComponent<MeshRenderer>().materials;
        Destroy(currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls[0].gameObject);
        currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.RemoveAt(0);
        AIBall.SetActive(true);
        for (int i = 0; i < currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.Count; i++)
        {
            currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls[i].transform.DOLocalMoveZ(z, 0.25f);
            z -= 1.2f;
            if (currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.Count == 0) return;
        }
        currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().posZ += 1.2f;
    }

    void AIGoBowlingArea()
    {
        if (AIBall.activeInHierarchy)
            navMeshAgent.SetDestination(throwBallArea.transform.position);
    }

    IEnumerator AIPlayBowling()
    {
        isOnPlayBowling = true;
        anim.SetTrigger("ThrowBall");
        yield return new WaitForSeconds(1f);
        var ball = Instantiate(AIBall, AIBall.transform.position, Quaternion.identity);
        ball.transform.localScale = new Vector3(70, 70, 70);
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.transform.DOLocalMoveZ(100, 3).SetEase(Ease.Linear);
        ball.transform.parent = null;
        ball.GetComponent<Rigidbody>().freezeRotation = false;
        ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        ball.GetComponent<Rigidbody>().useGravity = true;

        AIBall.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        AIBall.SetActive(false);
        AIBall.GetComponent<MeshRenderer>().enabled = true;
        playTime++;
        yield return new WaitForSeconds(4f);
        waitForPlay = false;
        isOnPlayBowling = false;
    }

    void Bowling()
    {
        if (!isPlayeable)
            return;
        if (currentArea == null)
            return;
        if (playTime < 2)
        {
            if (!AIBall.activeInHierarchy && currentArea.transform.Find("BowlingArea").Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.Count > 0)
            {
                if (waitForPlay)
                    return;
                navMeshAgent.SetDestination(pickBallArea.transform.position);
            }else
            {
                AIGoBowlingArea();
            }
        }
        else if (playTime >= 2)
        {
            isPlayeable = false;
            navMeshAgent.SetDestination(exit.position);
            if (bodyguard1 == null)
                return;
            StartCoroutine(SetBodyGuard());
            areaType = AreaType.none;
        }
    }

    IEnumerator SetBodyGuard()
    {
        yield return new WaitForSeconds(2f);
        bodyguard1.SetDestination(exit.position);
        yield return new WaitForSeconds(2f);
        bodyguard2.SetDestination(exit.position);
    }
}