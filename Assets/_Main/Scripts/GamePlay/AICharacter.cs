using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    private enum AIStates { Decision, GetBall, GettingBall, Fill, FillingBall }
    private AIStates aiState;

    [SerializeField] internal List<GameObject> bowlingAreas = new List<GameObject>();

    private NavMeshAgent navMeshAgent;

    internal List<GameObject> stackingBallList = new List<GameObject>();
    internal List<GameObject> stackingShoesList = new List<GameObject>();
    public int maxBallCount = 3;
    public int maxShoesCount = 3;
    private int limitToGetBall = 2;
    internal float lastCollectTime;
    internal GameObject ballStackPoint;

    private Transform ballPoint;
    private Transform fillPoint;
    private Vector3 fillPosition;

    private BallDistributorManager ballDistributorManager;

    private Animator anim;
    float timer;
    private void Start()
    {
        anim = GetComponent<Animator>();
        foreach (var item in new List<GameObject>(GameObject.FindGameObjectsWithTag("NewArea")))
        {
            if (!item.transform.Find("Canvas").gameObject.activeInHierarchy)
                bowlingAreas.Add(item);
        }

        List<GameObject> temp = new List<GameObject>();

        stackingBallList = new List<GameObject>();
        stackingShoesList = new List<GameObject>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        FindShelf();
        ballStackPoint = transform.Find("MoneyStackPoint").gameObject;
    }

    void FindShelf()
    {
        switch (Random.Range(0,2))
        {
            case 0:
                if (GameObject.Find("Shelf3").transform.Find("Shelfs").gameObject.activeInHierarchy)
                {
                    ballPoint = GameObject.Find("Shelf3").transform.Find("Shelfs").Find("Canvas").transform.GetChild(0);
                    ballDistributorManager = GameObject.Find("Shelf3").transform.Find("Shelfs").GetComponent<BallDistributorManager>();
                }
                break;
            case 1:
                if (GameObject.Find("Shelf2").transform.Find("Shelfs").gameObject.activeInHierarchy)
                {
                    ballPoint = GameObject.Find("Shelf2").transform.Find("Shelfs").Find("Canvas").transform.GetChild(0);
                    ballDistributorManager = GameObject.Find("Shelf2").transform.Find("Shelfs").GetComponent<BallDistributorManager>();
                }
                break;
        }
        if(ballPoint == null)
        {
            if (GameObject.Find("Shelf1").transform.Find("Shelfs").gameObject.activeInHierarchy)
            {
                ballPoint = GameObject.Find("Shelf1").transform.Find("Shelfs").Find("Canvas").transform.GetChild(0);
                ballDistributorManager = GameObject.Find("Shelf1").transform.Find("Shelfs").GetComponent<BallDistributorManager>();
            }
        }
    }

    private void Update()
    {
        anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        if (aiState == AIStates.Decision)
            Decision();
        else if (aiState == AIStates.GetBall)
            GetBall();
        else if (aiState == AIStates.GettingBall)
            CollectBall();
        else if (aiState == AIStates.Fill)
            Fill();
        else if (aiState == AIStates.FillingBall)
            FillBalls();
    }

    private void Decision()
    {
        if (stackingBallList.Count < limitToGetBall)
        {
            aiState = AIStates.GetBall;
        }
        else
        {
            for (int i = 0; i < bowlingAreas.Count; i++)
            {
                if (bowlingAreas[i].transform.Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.Count < 6)
                {
                    fillPoint = bowlingAreas[i].transform.Find("BallCollector").Find("BallCollectorArea");
                    fillPosition = fillPoint.position;
                    aiState = AIStates.Fill;
                }
            }

            if (aiState != AIStates.Fill)
            {
                if (stackingBallList.Count >= maxBallCount)
                {
                    if (Time.time - timer > 3)
                    {
                        int temp = Random.Range(0, 4);
                        switch (temp)
                        {
                            case 0:
                                navMeshAgent.SetDestination(Vector3.zero);
                                break;
                            case 1:
                                navMeshAgent.SetDestination(new Vector3(40, 0, 18));
                                break;
                            case 2:
                                navMeshAgent.SetDestination(new Vector3(-15, 0, 8));
                                break;
                            case 3:
                                navMeshAgent.SetDestination(new Vector3(51, 0, 12));
                                break;
                            default:
                                break;
                        }
                        timer = Time.time;
                    }
                }
                else
                {
                    aiState = AIStates.GetBall;
                }

            }
        }
    }

    private void GetBall()
    {
        navMeshAgent.SetDestination(new Vector3(ballPoint.position.x, transform.position.y, ballPoint.position.z));

        if (Mathf.Abs(Vector3.Distance(transform.position, new Vector3(ballPoint.position.x, transform.position.y, ballPoint.position.z))) < 0.75f)
        {
            aiState = AIStates.GettingBall;
        }
    }

    private void Fill()
    {
        navMeshAgent.SetDestination(fillPosition);

        if (Mathf.Abs(Vector3.Distance(transform.position, new Vector3(fillPoint.position.x, transform.position.y, fillPoint.position.z))) < 0.75f)
        {
            aiState = AIStates.FillingBall;
        }
    }

    public void OpenedNewBowlingArea(GameObject newArea)
    {
        bowlingAreas.Add(newArea);
    }

    private void CollectBall()
    {
        if (stackingBallList.Count >= maxBallCount)
        {
            aiState = AIStates.Decision;
            return;
        }
        if (Time.time - lastCollectTime > 0.3f)
        {
            ballDistributorManager.GetBall(true, this);
            lastCollectTime = Time.time;
        }

    }

    private void FillBalls()
    {
        if (stackingBallList.Count == 0 || fillPoint.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.Count == 6)
        {
            aiState = AIStates.Decision;
            return;
        }

        lastCollectTime = Time.time;
        stackingBallList[stackingBallList.Count - 1].GetComponent<BallMovement>().enabled = false;
        stackingBallList[stackingBallList.Count - 1].transform.parent = fillPoint.transform.parent.Find("RefBall").transform;
        stackingBallList[stackingBallList.Count - 1].transform.DOMove(new Vector3(stackingBallList[stackingBallList.Count - 1].transform.parent.position.x, stackingBallList[stackingBallList.Count - 1].transform.parent.position.y + 0.4f, stackingBallList[stackingBallList.Count - 1].transform.parent.position.z + fillPoint.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().posZ), 0.4f);
        fillPoint.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.Add(stackingBallList[stackingBallList.Count - 1]);
        fillPoint.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().posZ -= 1.2f;
        stackingBallList.Remove(stackingBallList[stackingBallList.Count - 1]);
    }
}