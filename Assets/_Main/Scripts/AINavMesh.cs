using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    public AreaType areaType;

    CustomerManager customerManager;
    CleanArea cleanArea;

    private NavMeshAgent navMeshAgent;
    public float radius;

    private Transform exit;
    internal GameObject currentArea = null;
    internal bool isPlayable = true;

    private int playTime = 0;
    private GameObject AIBall;
    private Vector3 bowlingTarget;
    private Vector3 bowlingAreaTarget;
    private GameObject bowlingShoesRight;
    private GameObject bowlingShoesLeft;

    internal float playLangertTime;
    internal Transform langertPoint1, langertPoint2;

    internal float playBillardTime;
    internal Transform billardStickPoint, billardStickPoint2;
    internal Transform point1, point2;

    internal float kafeTime;
    internal float kafeChairTime;
    internal float kafeChairTimeTwo;
    internal float kafeChairTimeThree;
    internal Transform kafePoint1, kafePoint2, kafePoint3, kafePoint4, kafePoint5, kafePoint6,kafePoint7;

    internal float sitTime;

    internal float playDartTime;
    internal float arcadeMachineTime;
    float playTennisTime;

    private Animator anim;

    public GameObject moneyPrefab;
    public ParticleSystem shoesVfx;
    private GameObject cleanAreaLookAt;

    bool isOnPlayBowling = false;
    Transform currentChair = null;
    bool waitForPlay = false;

    public GameObject dart;
    public GameObject racket;

    private void Start()
    {
        kafePoint1 = GameObject.Find("CaffeArea2").transform.Find("Point1").transform;
        kafePoint2 = GameObject.Find("CaffeArea2").transform.Find("Point2").transform;
        kafePoint3 = GameObject.Find("CaffeArea2").transform.Find("Point3").transform;
        kafePoint4 = GameObject.Find("CaffeArea2").transform.Find("Point4").transform;
        kafePoint5 = GameObject.Find("CaffeArea2").transform.Find("Point5").transform;
        kafePoint6 = GameObject.Find("CaffeArea2").transform.Find("Point6").transform;
        kafePoint7 = GameObject.Find("CaffeArea2").transform.Find("Point7").transform;

        cleanAreaLookAt = GameObject.Find("CleaningArea").transform.Find("AILook").gameObject;

        anim = GetComponent<Animator>();
        customerManager = FindObjectOfType<CustomerManager>();
        cleanArea = FindObjectOfType<CleanArea>();
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if(transform.Find("Armature") != null)
        {
            AIBall = transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("mixamorig:RightHandMiddle2").Find("mixamorig:RightHandMiddle3").Find("Ball").gameObject;
            bowlingShoesRight = transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:RightUpLeg").Find("mixamorig:RightLeg").Find("mixamorig:RightFoot").Find("mixamorig:RightToeBase").Find("RightBowlingShoes").gameObject;
            bowlingShoesLeft = transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:LeftUpLeg").Find("mixamorig:LeftLeg").Find("mixamorig:LeftFoot").Find("mixamorig:LeftToeBase").Find("LeftBowlingShoes").gameObject;

        }
        else
        {
            AIBall = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("mixamorig:RightHandMiddle2").Find("mixamorig:RightHandMiddle3").Find("Ball").gameObject;
            bowlingShoesRight = transform.Find("mixamorig:Hips").Find("mixamorig:RightUpLeg").Find("mixamorig:RightLeg").Find("mixamorig:RightFoot").Find("mixamorig:RightToeBase").Find("mixamorig:RightToe_End").Find("RightBowlingShoes").gameObject;
            bowlingShoesLeft = transform.Find("mixamorig:Hips").Find("mixamorig:LeftUpLeg").Find("mixamorig:LeftLeg").Find("mixamorig:LeftFoot").Find("mixamorig:LeftToeBase").Find("mixamorig:LeftToe_End").Find("LeftBowlingShoes").gameObject;
        }

        exit = GameObject.Find("CustomerExit").transform;
    }

    private void Update()
    {
        anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        switch (areaType)
        {
            case AreaType.bowling:
                Bowling(currentArea.GetComponent<BowlingArea>().playCountPerCustomer);
                break;
            case AreaType.langert:
                Langert(30);
                break;
            case AreaType.bilardo:
                Billard(35);
                break;
            case AreaType.kafeterya:
                Kafeterya(45);
                KafeteryaChair(45);
                KafeteryaChairTwo(45);
                KafeteryaChairThree(45);
                break;
            case AreaType.none:
                break;
            case AreaType.cleanShoes:
                ShoesArea();
                break;
            case AreaType.dart:
                PlayDart(60);
                break;
            case AreaType.miniCafe:
                MiniCafeArea(45);
                break;
            case AreaType.tableTennis:
                PlayTennis(35);
                break;
            case AreaType.arcadeMachine:
                PlayArcadeMachine(40);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tags.BallPlatform:
                AIPickBall(other);
                break;
            case Tags.BowlingAreaTarget:
                anim.SetTrigger("ThrowBall");
                StartCoroutine(AIPlayBowling(other));
                break;
            case Tags.ShoesArea:
                transform.Find("ShoesVfx").GetComponent<ParticleSystem>().Play();
                bowlingShoesLeft.SetActive(true);
                bowlingShoesRight.SetActive(true);
                cleanArea.transform.Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
                areaType = AreaType.bowling;
                break;
            case Tags.DropShoes:
                if (bowlingShoesLeft.activeInHierarchy && bowlingShoesRight.activeInHierarchy)
                {
                    bowlingShoesLeft.SetActive(false);
                    bowlingShoesRight.SetActive(false);
                }
                break;
            case Tags.Exit:
                customerManager.customers.Remove(this.gameObject);
                Destroy(this.gameObject);
                break;
            case Tags.Chair:
                if (currentChair != null && currentChair == other.transform.parent)
                {
                    transform.LookAt(new Vector3(other.transform.Find("LookPoint").transform.position.x, transform.position.y, other.transform.Find("LookPoint").transform.position.z));
                    anim.SetBool("isSitting", true);
                }
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.LangertTime))
        {
            playLangertTime += Time.deltaTime;
        }
        if (other.CompareTag(Tags.BillardTimer))
        {

            playBillardTime += Time.deltaTime;
        }
        if (currentChair != null && other.CompareTag(Tags.Chair) && currentChair == other.transform.parent)
        {
            transform.LookAt(new Vector3(other.transform.Find("LookPoint").transform.position.x, transform.position.y, other.transform.Find("LookPoint").transform.position.z));
        }
    }

    void ShoesArea()
    {
        if (currentArea == null) return;

        float x = -35f;
        for (int i = 0; i < cleanArea.shoesCustomers.Count; i++)
        {
            cleanArea.shoesCustomers[i].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, 4.275599f, 4));
            if(cleanArea.shoesCustomers[i].transform.position.x == x)
            {
                cleanArea.shoesCustomers[i].transform.LookAt(cleanAreaLookAt.transform);
            }
            x += 3;
        }
    }


    IEnumerator AIGetBall()
    {
        if (!AIBall.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(1f);
            navMeshAgent.SetDestination(bowlingTarget);
        }
    }

    void AIPickBall(Collider other)
    {
        float z = 0;
        AIBall.GetComponent<MeshRenderer>().materials = other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls[0].GetComponent<MeshRenderer>().materials;    
        Destroy(other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls[0].gameObject);
        other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.RemoveAt(0);
        AIBall.SetActive(true);
        for (int i = 0; i < other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.Count; i++)
        {
            other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls[i].transform.DOLocalMoveZ(z, 0.25f);
            z -= 1.2f;
            if (other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.Count == 0) return;
        }
        other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().posZ += 1.2f;
    }

    void AIGoBowlingArea()
    {
        if (AIBall.activeInHierarchy)
            navMeshAgent.SetDestination(bowlingAreaTarget);
    }

    IEnumerator AIPlayBowling(Collider other)
    {
        yield return new WaitForSeconds(0.8f);
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
        waitForPlay = true;
        StartCoroutine(YouCanPlay());
        StartCoroutine(SetPlay());
        isOnPlayBowling = false;
    }

    IEnumerator SetPlay()
    {
        yield return new WaitForSeconds(1f);
        if(currentArea != null)
            currentArea.GetComponent<BowlingArea>().isOnPlayOne = false;
    }

    IEnumerator YouCanPlay()
    {
        yield return new WaitForSeconds(3f * currentArea.GetComponent<BowlingArea>().currentCustomerCount);
        waitForPlay = false;
    }

    void Bowling(int playBowling)
    {
        if (currentArea == null) return;
        if (playTime < playBowling)
        {
            //Go get ball
            if (!AIBall.activeInHierarchy && currentArea.transform.Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls.Count > 0)
            {
                if ((!currentArea.GetComponent<BowlingArea>().isOnPlayOne || isOnPlayBowling) && !waitForPlay)
                {
                    anim.SetBool("isSitting", false);
                    isOnPlayBowling = true;
                    currentArea.GetComponent<BowlingArea>().isOnPlayOne = true;
                    bowlingTarget = currentArea.transform.Find("BallCollector").Find("RefBall").GetComponent<BallCollectorManager>().balls[0].transform.position;
                    bowlingAreaTarget = currentArea.transform.Find("AITrigger").position;
                    StartCoroutine(AIGetBall());
                }else
                {
                    if(currentChair == null)
                    {
                        Transform temp = currentArea.GetComponent<BowlingArea>().waitingAreas.Find(x => !x.GetComponent<WaitingArea>().isFill);
                        if (temp != null)
                        {
                            anim.SetBool("isSitting", false);
                            currentChair = temp;
                            temp.GetComponent<WaitingArea>().isFill = true;
                            navMeshAgent.SetDestination(temp.position);
                        }
                    }
                    else
                    {
                        navMeshAgent.SetDestination(currentChair.position);
                    }
                }
            }
            //go to chair
            else if (!AIBall.activeInHierarchy)
            {
                if (currentChair == null)
                {
                    Transform temp = currentArea.GetComponent<BowlingArea>().waitingAreas.Find(x => !x.GetComponent<WaitingArea>().isFill);
                    if (temp != null)
                    {
                        currentChair = temp;
                        temp.GetComponent<WaitingArea>().isFill = true;
                        navMeshAgent.SetDestination(temp.position);
                    }
                }else
                {
                    navMeshAgent.SetDestination(currentChair.position);
                }
            }
            //If you have a ball go bowling throwing area
            else
            {
                AIGoBowlingArea();
            }
        }
        //if play time done
        else if (playTime == playBowling)
        {
            if(currentChair != null)
            {
                currentChair.GetComponent<WaitingArea>().isFill = false;
                currentChair = null;
            }
            AIBall.SetActive(false);
            anim.SetBool("isSitting", false);
            navMeshAgent.SetDestination(exit.position);
            currentArea.GetComponent<BowlingArea>().currentCustomerCount--;
            transform.gameObject.tag = "Waiter";
            customerManager.customers.Remove(this.gameObject);
            currentArea.GetComponent<BowlingArea>().isOnPlayOne = false;
            currentArea = null;
            areaType = AreaType.none;
            playTime = 0;
        }
    }

    void Kafeterya(float kafeteryaTime)
    {
        if (currentArea == null) return;

        Vector3 point;

        if(kafeTime < kafeteryaTime)
        {
            if (currentArea.GetComponent<CafeArea>().cafeCustomers.Count > 0)
            {
                if (currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<Animator>().GetBool("isSitting") == true)
                {
                    currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<AINavMesh>().kafeTime += Time.deltaTime;
                }
                point = kafePoint1.transform.position;
                if (currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<NavMeshAgent>().enabled == false) return;
                currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<NavMeshAgent>().SetDestination(point);
                if (currentArea.GetComponent<CafeArea>().cafeCustomers[0].transform.position.x == point.x)
                {
                    point = new Vector3(currentArea.transform.Find("BarChair").position.x, 2, currentArea.transform.Find("BarChair").position.z);
                    currentArea.GetComponent<CafeArea>().cafeCustomers[0].transform.position = point;
                    if(currentArea.GetComponent<CafeArea>().cafeCustomers[0].transform.position.x == point.x)
                    {
                        currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<NavMeshAgent>().Stop();
                        currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<NavMeshAgent>().enabled = false;
                        currentArea.GetComponent<CafeArea>().cafeCustomers[0].transform.position = point;
                        currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<Animator>().SetBool("isSitting", true);
                    }
                }
            }
        }
        if (kafeTime > kafeteryaTime)
        {
            currentArea.transform.Find("Monies").gameObject.SetActive(true);
            currentArea.transform.Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
            if (currentArea.GetComponent<CafeArea>().cafeCustomers.Count > 0)
            {
                currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<NavMeshAgent>().enabled = true;
                currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<Animator>().SetBool("isSitting", false);
                currentArea.GetComponent<CafeArea>().cafeCustomers[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
            }
            currentArea.GetComponent<CafeArea>().currentCafeCustomer = 0;
            GetComponent<AINavMesh>().kafeTime = 0;
            customerManager.customers.Remove(this.gameObject);
            currentArea.GetComponent<CafeArea>().cafeCustomers.Clear();
        }
    }

    void KafeteryaChair(float time)
    {
        if (currentArea == null) return;

        Vector3 point;
        if (kafeChairTime < time)
        {
            if (currentArea.GetComponent<CafeArea>().cafeChair1.Count > 0)
            {
                if (currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<Animator>().GetBool("isSitting") == true)
                {
                    currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<AINavMesh>().kafeChairTime += Time.deltaTime;
                }
                point = kafePoint4.transform.position;
                if (currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<NavMeshAgent>().enabled == false) return;
                currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<NavMeshAgent>().SetDestination(point);
                if (currentArea.GetComponent<CafeArea>().cafeChair1[0].transform.position.x == point.x)
                {
                    point = kafePoint2.transform.position;
                    currentArea.GetComponent<CafeArea>().cafeChair1[0].transform.position = point;
                    if (currentArea.GetComponent<CafeArea>().cafeChair1[0].transform.position.x == point.x)
                    {
                        currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<NavMeshAgent>().Stop();
                        currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<NavMeshAgent>().enabled = false;
                        currentArea.GetComponent<CafeArea>().cafeChair1[0].transform.position = point;
                        currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<Animator>().SetBool("isSitting", true);
                    }
                }
            }
        }

        if (kafeChairTime > time)
        {
            currentArea.transform.Find("Monies").gameObject.SetActive(true);
            currentArea.transform.Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);

            if (currentArea.GetComponent<CafeArea>().cafeChair1.Count > 0)
            {
                currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<NavMeshAgent>().enabled = true;
                currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<Animator>().SetBool("isSitting", false);
                currentArea.GetComponent<CafeArea>().cafeChair1[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
            }
            GetComponent<AINavMesh>().kafeChairTime = 0;
            currentArea.GetComponent<CafeArea>().currentCafeChair1 = 0;
            customerManager.customers.Remove(this.gameObject);
            currentArea.GetComponent<CafeArea>().cafeChair1.Clear();
        }
    }

    void KafeteryaChairTwo(float time)
    {
        if (currentArea == null) return;

        Vector3 point;

        if (kafeChairTimeTwo < time)
        {
            if (currentArea.GetComponent<CafeArea>().cafeChair2.Count > 0)
            {
                if (currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<Animator>().GetBool("isSitting") == true)
                {
                    currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<AINavMesh>().kafeChairTimeTwo += Time.deltaTime;
                }
                point = kafePoint5.transform.position;
                if (currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<NavMeshAgent>().enabled == false) return;
                currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<NavMeshAgent>().SetDestination(point);
                if (currentArea.GetComponent<CafeArea>().cafeChair2[0].transform.position.x == point.x)
                {
                    point = kafePoint3.transform.position;
                    currentArea.GetComponent<CafeArea>().cafeChair2[0].transform.position = point;
                    if (currentArea.GetComponent<CafeArea>().cafeChair2[0].transform.position.x == point.x)
                    {
                        currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<NavMeshAgent>().Stop();
                        currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<NavMeshAgent>().enabled = false;
                        currentArea.GetComponent<CafeArea>().cafeChair2[0].transform.position = point;
                        currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<Animator>().SetBool("isSitting", true);
                    }
                }
            }
        }

        if (kafeChairTimeTwo > time)
        {
            currentArea.transform.Find("Monies").gameObject.SetActive(true);
            currentArea.transform.Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
            if (currentArea.GetComponent<CafeArea>().cafeChair2.Count > 0)
            {
                currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<NavMeshAgent>().enabled = true;
                currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<Animator>().SetBool("isSitting", false);
                currentArea.GetComponent<CafeArea>().cafeChair2[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
            }
            GetComponent<AINavMesh>().kafeChairTimeTwo = 0;
            currentArea.GetComponent<CafeArea>().currentCafeChair2 = 0;
            customerManager.customers.Remove(this.gameObject);
            currentArea.GetComponent<CafeArea>().cafeChair2.Clear();
        }
    }

    void KafeteryaChairThree(float time)
    {
        if (currentArea == null) return;

        Vector3 point;

        if (kafeChairTimeThree < time)
        {
            if (currentArea.GetComponent<CafeArea>().cafeChair3.Count > 0)
            {
                if (currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<Animator>().GetBool("isSitting") == true)
                {
                    currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<AINavMesh>().kafeChairTimeThree += Time.deltaTime;
                }
                point = kafePoint6.transform.position;
                if (currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<NavMeshAgent>().enabled == false) return;
                currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<NavMeshAgent>().SetDestination(point);
                if (currentArea.GetComponent<CafeArea>().cafeChair3[0].transform.position.x == point.x)
                {
                    point = kafePoint7.transform.position;
                    currentArea.GetComponent<CafeArea>().cafeChair3[0].transform.position = point;
                    if (currentArea.GetComponent<CafeArea>().cafeChair3[0].transform.position.x == point.x)
                    {
                        currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<NavMeshAgent>().Stop();
                        currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<NavMeshAgent>().enabled = false;
                        currentArea.GetComponent<CafeArea>().cafeChair3[0].transform.position = point;
                        currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<Animator>().SetBool("isSitting", true);
                    }
                }
            }
        }

        if (kafeChairTimeThree > time)
        {
            currentArea.transform.Find("Monies").gameObject.SetActive(true);
            currentArea.transform.Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
            if (currentArea.GetComponent<CafeArea>().cafeChair3.Count > 0)
            {
                currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<NavMeshAgent>().enabled = true;
                currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<Animator>().SetBool("isSitting", false);
                currentArea.GetComponent<CafeArea>().cafeChair3[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
            }
            GetComponent<AINavMesh>().kafeChairTimeThree = 0;
            currentArea.GetComponent<CafeArea>().currentCafeChair3 = 0;
            customerManager.customers.Remove(this.gameObject);
            currentArea.GetComponent<CafeArea>().cafeChair3.Clear();
        }
    }
    void Langert(float playLangert)
    {
        if (currentArea == null) return;
        if(playLangertTime < playLangert)
        {
            langertPoint1 = currentArea.transform.Find("Point1").transform;
            langertPoint2 = currentArea.transform.Find("Point2").transform;

            if(currentArea.GetComponent<LangertArea>().langertCustomers.Count > 0)
            {
                currentArea.GetComponent<LangertArea>().langertCustomers[0].GetComponent<NavMeshAgent>().SetDestination(langertPoint1.position);
                if(currentArea.GetComponent<LangertArea>().langertCustomers[0].transform.position.x == langertPoint1.position.x)
                {
                    currentArea.GetComponent<LangertArea>().langertCustomers[0].transform.LookAt(currentArea.transform.Find("LookAt").transform.position);
                    currentArea.GetComponent<LangertArea>().langertCustomers[0].GetComponent<Animator>().SetBool("Langert", true);
                }
            }
            if (currentArea.GetComponent<LangertArea>().langertCustomers.Count > 1)
            {
                currentArea.GetComponent<LangertArea>().langertCustomers[1].GetComponent<NavMeshAgent>().SetDestination(langertPoint2.position);
                if (currentArea.GetComponent<LangertArea>().langertCustomers[1].transform.position.x == langertPoint2.position.x)
                {
                    currentArea.GetComponent<LangertArea>().langertCustomers[1].transform.LookAt(currentArea.transform.Find("LookAt").transform.position);
                    currentArea.GetComponent<LangertArea>().langertCustomers[1].GetComponent<Animator>().SetBool("Langert", true);
                }
            }
        }
        if(playLangertTime > playLangert)
        {
            if (currentArea.GetComponent<LangertArea>().langertCustomers.Count > 0)
            {
                currentArea.GetComponent<LangertArea>().langertCustomers[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
                currentArea.GetComponent<LangertArea>().langertCustomers[0].GetComponent<AINavMesh>().areaType = AreaType.none;
                currentArea.GetComponent<LangertArea>().langertCustomers[0].gameObject.tag = "Untagged";
                customerManager.customers.Remove(currentArea.GetComponent<LangertArea>().langertCustomers[0]);
                currentArea.GetComponent<LangertArea>().langertCustomers[0].GetComponent<Animator>().SetBool("Langert", false);
                currentArea.GetComponent<LangertArea>().langertCustomers[0].GetComponent<AINavMesh>().playLangertTime = 0;
            }
            if (currentArea.GetComponent<LangertArea>().langertCustomers.Count > 1)
            {
                currentArea.GetComponent<LangertArea>().langertCustomers[1].GetComponent<NavMeshAgent>().SetDestination(exit.position);
                currentArea.GetComponent<LangertArea>().langertCustomers[1].GetComponent<AINavMesh>().areaType = AreaType.none;
                currentArea.GetComponent<LangertArea>().langertCustomers[1].gameObject.tag = "Untagged";
                customerManager.customers.Remove(currentArea.GetComponent<LangertArea>().langertCustomers[1]);
                currentArea.GetComponent<LangertArea>().langertCustomers[1].GetComponent<Animator>().SetBool("Langert", false);
                currentArea.GetComponent<LangertArea>().langertCustomers[1].GetComponent<AINavMesh>().playLangertTime = 0;
            }
            currentArea.GetComponent<LangertArea>().currentCustomer = 0;
            currentArea.GetComponent<LangertArea>().langertCustomers.Clear();
            if(currentArea.transform.Find("Unlock").gameObject.activeInHierarchy)
            {
                currentArea.transform.Find("Unlock").Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
            }else
            {
                currentArea.transform.Find("UnlockVIP").Find("Monies").GetComponent<Moneys>().SetMoney(6, gameObject);
            }
        }
    }

    void PlayTennis(float tennisTime)
    {
        if (currentArea == null) return;
        if(playTennisTime < tennisTime)
        {
            for (int i = 0; i < currentArea.GetComponent<TableTennis>().tableTennisCustomers.Count; i++)
            {
                currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].GetComponent<NavMeshAgent>().SetDestination(currentArea.transform.Find("Point" + i).transform.position);
                if(currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].transform.position.x == currentArea.transform.Find("Point" + i).transform.position.x)
                {
                    currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].GetComponent<AINavMesh>().racket.SetActive(true);
                    currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].GetComponent<Animator>().SetBool("isPlayTennis", true);
                    playTennisTime += Time.deltaTime;
                }
            }
        }
        else if(playTennisTime > tennisTime)
        {
            for (int i = 0; i < currentArea.GetComponent<TableTennis>().tableTennisCustomers.Count; i++)
            {
                currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].GetComponent<NavMeshAgent>().SetDestination(exit.transform.position);
                currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].GetComponent<AINavMesh>().racket.SetActive(false);
                currentArea.GetComponent<TableTennis>().tableTennisCustomers[i].GetComponent<Animator>().SetBool("isPlayTennis", false);
            }
            tag = "Untagged";
            currentArea.GetComponent<TableTennis>().currentCustomer = 0;
            currentArea.GetComponent<TableTennis>().tableTennisCustomers.Clear();
            if (currentArea.transform.Find("Unlock").gameObject.activeInHierarchy)
            {
                currentArea.transform.Find("Unlock").Find("Monies").GetComponent<Moneys>().SetMoney(4, gameObject);
            }
            else
            {
                currentArea.transform.Find("UnlockVIP").Find("Monies").GetComponent<Moneys>().SetMoney(8, gameObject);
            }
            customerManager.customers.Remove(gameObject);
            currentArea = null;
        }
    }
    void PlayDart(float dartTime)
    {
        if (currentArea == null) return;
        if(playDartTime < dartTime)
        {
            navMeshAgent.SetDestination(currentArea.transform.Find("AIPoint").transform.position);
            if(transform.position.x == currentArea.transform.Find("AIPoint").transform.position.x)
            {
                transform.LookAt(currentArea.transform.Find("AILookAt").transform);
                playDartTime += Time.deltaTime;
                dart.SetActive(true);
                anim.SetBool("isPlayDart", true);
                //Animasyon calisicak
            }
        }
        else if(playDartTime >= dartTime)
        {
            dart.SetActive(false);
            anim.SetBool("isPlayDart", false);
            navMeshAgent.SetDestination(exit.transform.position);
            tag = "Untagged";
            currentArea.GetComponent<DartArea>().currentCustomer = 0;
            currentArea.GetComponent<DartArea>().dartCustomers.Clear();
            if (currentArea.transform.Find("Unlock").gameObject.activeInHierarchy)
            {
                currentArea.transform.Find("Unlock").Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
            }
            else
            {
                currentArea.transform.Find("UnlockVIP").Find("Monies").GetComponent<Moneys>().SetMoney(6, gameObject);
            }
            customerManager.customers.Remove(gameObject);
            currentArea = null;
        }
    }
    void PlayArcadeMachine(float arcadeTime)
    {
        if (currentArea == null) return;
        if (arcadeMachineTime < arcadeTime)
        {
            navMeshAgent.SetDestination(currentArea.transform.Find("AIPoint").transform.position);
            if (transform.position.x == currentArea.transform.Find("AIPoint").transform.position.x)
            {
                transform.LookAt(currentArea.transform.Find("AILookAt").transform);
                arcadeMachineTime += Time.deltaTime;
                dart.SetActive(true);
                anim.SetBool("isPlayDart", true);
                //Animasyon calisicak
            }
        }
        else if (arcadeMachineTime >= arcadeTime)
        {
            dart.SetActive(false);
            anim.SetBool("isPlayDart", false);
            navMeshAgent.SetDestination(exit.transform.position);
            tag = "Untagged";
            currentArea.GetComponent<ArcadeArea>().currentCustomer = 0;
            currentArea.GetComponent<ArcadeArea>().dartCustomers.Clear();
            if (currentArea.transform.Find("Unlock").gameObject.activeInHierarchy)
            {
                currentArea.transform.Find("Unlock").Find("Monies").GetComponent<Moneys>().SetMoney(3, gameObject);
            }
            else
            {
                currentArea.transform.Find("UnlockVIP").Find("Monies").GetComponent<Moneys>().SetMoney(6, gameObject);
            }
            customerManager.customers.Remove(gameObject);
            currentArea = null;
        }
    }


    void MiniCafeArea(float cafeTime)
    {
        Vector3 point;
        if (currentArea == null) return;
        if (sitTime < cafeTime)
        {
            for (int i = 0; i < currentArea.GetComponent<MiniCafeArea>().cafeCustomers.Count; i++)
            {
                if (currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<Animator>().GetBool("isSitting") == true)
                {
                    currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<AINavMesh>().sitTime += Time.deltaTime;
                }
                if (currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<NavMeshAgent>().enabled != false)
                {
                    currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<NavMeshAgent>().SetDestination(currentArea.transform.Find("MiniCafeAreaChilds").Find("Point" + i).transform.position);
                    if (Mathf.Abs(Vector3.Distance(transform.position, new Vector3(currentArea.transform.Find("MiniCafeAreaChilds").Find("Point" + i).transform.position.x, transform.position.y, currentArea.transform.Find("MiniCafeAreaChilds").Find("Point" + i).transform.position.z))) < 0.75f)
                    {
                        point = new Vector3(currentArea.transform.Find("MiniCafeAreaChilds").Find("BarChair" + i).transform.position.x, 3, currentArea.transform.Find("MiniCafeAreaChilds").Find("BarChair" + i).transform.position.z);
                        currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].transform.position = point;
                        if (currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].transform.position.x == point.x)
                        {
                            currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<NavMeshAgent>().Stop();
                            currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].transform.LookAt(currentArea.transform.Find("MiniCafeAreaChilds").Find("BarChair" + i).Find("LookPoint").transform);
                            currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<NavMeshAgent>().enabled = false;
                            currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].transform.position = point;
                            currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<Animator>().SetBool("isSitting", true);
                        }
                    }
                }
                if (currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<AINavMesh>().sitTime > cafeTime)
                {
                    currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<Animator>().SetBool("isSitting", false);
                    currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<NavMeshAgent>().enabled = true;
                    currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i].GetComponent<NavMeshAgent>().SetDestination(exit.transform.position);
                    currentArea.GetComponent<MiniCafeArea>().currentCafeCustomer--;
                    customerManager.customers.Remove(currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i]);
                    currentArea.GetComponent<MiniCafeArea>().cafeCustomers.Remove(currentArea.GetComponent<MiniCafeArea>().cafeCustomers[i]);
                    currentArea.transform.Find("Monies").gameObject.SetActive(true);
                    currentArea.transform.Find("Monies").GetComponent<Moneys>().SetMoney(2, gameObject);
                }
            }
        }
    }

    void Billard(float billardTime)
    {
        if (currentArea == null) return;

        if(playBillardTime < billardTime)
        {
            billardStickPoint = currentArea.transform.Find("BillardStickPoint").transform;
            billardStickPoint2 = currentArea.transform.Find("BillardStickPoint2").transform;
            point1 = currentArea.transform.Find("BillardStickPoint").Find("Point");
            point2 = currentArea.transform.Find("BillardStickPoint2").Find("Point");

            if (currentArea.GetComponent<BillardArea>().billardCustormers.Count > 0)
            {
                if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("Armature") != null)
                {
                    if (!currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                    .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<NavMeshAgent>().SetDestination(billardStickPoint.position);
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.position.x == billardStickPoint.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(true);
                            currentArea.transform.Find("Sticks").Find("BilliardsStick1").gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        AIOneWalkingBillardArea();
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.position.x == point1.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<Animator>().SetBool("playBilardo", true);
                            currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.LookAt(currentArea.transform.Find("LookAt"));
                        }
                    }
                }
                else
                {
                    if (!currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                    .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<NavMeshAgent>().SetDestination(billardStickPoint.position);
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.position.x == billardStickPoint.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(true);
                            currentArea.transform.Find("Sticks").Find("BilliardsStick1").gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        AIOneWalkingBillardArea();
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.position.x == point1.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<Animator>().SetBool("playBilardo", true);
                            currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.LookAt(currentArea.transform.Find("LookAt"));
                        }
                    }
                }
            }

            if (currentArea.GetComponent<BillardArea>().billardCustormers.Count > 1)
            {

                if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("Armature") != null)
                {
                    if (!currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                    .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<NavMeshAgent>().SetDestination(billardStickPoint2.position);
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.position.x == billardStickPoint2.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(true);
                            currentArea.transform.Find("Sticks").Find("BilliardsStick2").gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        AITwoWalkingBillardArea();
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.position.x == point2.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<Animator>().SetBool("playBilardo", true);
                            currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.LookAt(currentArea.transform.Find("LookAt"));
                        }
                    }
                }
                else
                {
                    if (!currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                    .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<NavMeshAgent>().SetDestination(billardStickPoint2.position);
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.position.x == billardStickPoint2.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                                .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(true);
                            currentArea.transform.Find("Sticks").Find("BilliardsStick2").gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        AITwoWalkingBillardArea();
                        if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.position.x == point2.position.x)
                        {
                            currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<Animator>().SetBool("playBilardo", true);
                            currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.LookAt(currentArea.transform.Find("LookAt"));
                        }
                    }
                }
            }
        }

        if (playBillardTime > billardTime)
        {
            if (currentArea.GetComponent<BillardArea>().billardCustormers.Count > 0)
            {
                currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<Animator>().SetBool("playBilardo", false);
                if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("Armature") != null)
                {
                    if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(false);
                        currentArea.transform.Find("Sticks").Find("BilliardsStick1").gameObject.SetActive(true);
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<AINavMesh>().areaType = AreaType.none;
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].tag = "Untagged";
                        customerManager.customers.Remove(currentArea.GetComponent<BillardArea>().billardCustormers[0].gameObject);
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<AINavMesh>().playBillardTime = 0;
                    }
                }
                else
                {
                    if (currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(false);
                        currentArea.transform.Find("Sticks").Find("BilliardsStick1").gameObject.SetActive(true);
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<NavMeshAgent>().SetDestination(exit.position);
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<AINavMesh>().areaType = AreaType.none;
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].tag = "Untagged";
                        customerManager.customers.Remove(currentArea.GetComponent<BillardArea>().billardCustormers[0].gameObject);
                        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<AINavMesh>().playBillardTime = 0;
                    }

                }
                if (currentArea.transform.Find("Unlock").gameObject.activeInHierarchy)
                {
                    currentArea.transform.Find("Unlock").Find("Monies").GetComponent<Moneys>().SetMoney(4, gameObject);
                }
                else
                {
                    currentArea.transform.Find("UnlockVIP").Find("Monies").GetComponent<Moneys>().SetMoney(8, gameObject);
                }
            }
            if (currentArea.GetComponent<BillardArea>().billardCustormers.Count > 1)
            {
                currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<Animator>().SetBool("playBilardo", false);
                if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("Armature") != null)
                {
                    if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("Armature").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(false);
                        currentArea.transform.Find("Sticks").Find("BilliardsStick2").gameObject.SetActive(true);
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<NavMeshAgent>().SetDestination(exit.position);
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<AINavMesh>().areaType = AreaType.none;
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].tag = "Untagged";
                        customerManager.customers.Remove(currentArea.GetComponent<BillardArea>().billardCustormers[0].gameObject);
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<AINavMesh>().playBillardTime = 0;
                    }
                }
                else
                {
                    if (currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.activeInHierarchy)
                    {
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm")
                        .Find("mixamorig:RightHand").Find("mixamorig:RightHandMiddle1").Find("BilliardsStick").gameObject.SetActive(false);
                        currentArea.transform.Find("Sticks").Find("BilliardsStick2").gameObject.SetActive(true);
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<NavMeshAgent>().SetDestination(exit.position);
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<AINavMesh>().areaType = AreaType.none;
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].tag = "Untagged";
                        customerManager.customers.Remove(currentArea.GetComponent<BillardArea>().billardCustormers[0].gameObject);
                        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<AINavMesh>().playBillardTime = 0;
                    }
                }
            }

            currentArea.GetComponent<BillardArea>().currentCustomer = 0;
            currentArea.GetComponent<BillardArea>().billardCustormers.Clear();
        }
    }

    void AIOneWalkingBillardArea()
    {
        currentArea.GetComponent<BillardArea>().billardCustormers[0].GetComponent<NavMeshAgent>().SetDestination(point1.position);
    }
    void AITwoWalkingBillardArea()
    {
        currentArea.GetComponent<BillardArea>().billardCustormers[1].GetComponent<NavMeshAgent>().SetDestination(point2.position);
    }
}

public enum AreaType
{
    bowling, langert, bilardo, kafeterya, none, cleanShoes, dart, miniCafe, tableTennis, arcadeMachine
}

public enum BowlingState
{
    goWait, goToBall, goChair
}
