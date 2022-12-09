using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDistributorManager : MonoBehaviour
{
    [SerializeField] internal List<GameObject> rafBalls = new List<GameObject>();

    BallCollector ballCollector;
    AICharacter aiCharacter;
    float lastInstantiateTime = 0;
    internal float instantFreq = 7;
    public List<GameObject> balls = new List<GameObject>();
    internal int level = 0;
    MyCollectionManager myCollectionManager;

    private void Awake()
    {
        myCollectionManager = FindObjectOfType<MyCollectionManager>();
        ballCollector = FindObjectOfType<BallCollector>();
        aiCharacter = FindObjectOfType<AICharacter>();
    }

    private void Start()
    {
        StartCoroutine(SetOpenUpgradeButton());
    }

    IEnumerator SetOpenUpgradeButton()
    {
        yield return new WaitForSeconds(10f);
        if (level < 2)
            transform.Find("UpgradeProductionTime").gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Time.time - lastInstantiateTime < (instantFreq - (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name) * 1.25f))) return;
        InstantiateBall();
    }
    
    void InstantiateBall()
    {
        GameObject temp;
        int lastOpenCollectionIndex = 1;


        if (myCollectionManager == null && FindObjectOfType<MyCollectionManager>() != null)
        {
            myCollectionManager = FindObjectOfType<MyCollectionManager>();
        }
        if (myCollectionManager != null)
            lastOpenCollectionIndex = myCollectionManager.lastOpenCollectionIndex;

        switch (level)
        {
            case 0:
                if (rafBalls.Count == 14) break;
                temp = Instantiate(balls[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Shelfs").Find("BallsLevel1").GetChild(14 - rafBalls.Count - 1).transform.position,
                    Quaternion.identity, transform.root.Find("Shelfs").Find("BallsLevel1New"));
                rafBalls.Add(temp);
                temp.transform.parent = transform.root.Find("Shelfs").Find("BallsLevel1New");
                temp.SetActive(true);
                lastInstantiateTime = Time.time;
                break;
            case 1:
                if (rafBalls.Count == 24) break;
                temp = Instantiate(balls[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Shelfs").Find("BallsLevel2").GetChild(24 - rafBalls.Count - 1).transform.position,
                    Quaternion.identity, transform.root.Find("Shelfs").Find("BallsLeve2New"));
                rafBalls.Add(temp);
                temp.transform.parent = transform.root.Find("Shelfs").Find("BallsLevel2New");
                temp.SetActive(true);
                lastInstantiateTime = Time.time;
                break;
            case 2:
                if (rafBalls.Count == 32) break;
                temp = Instantiate(balls[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Shelfs").Find("BallsLevel3").GetChild(32 - rafBalls.Count - 1).transform.position,
                    Quaternion.identity, transform.root.Find("Shelfs").Find("BallsLeve3New"));
                rafBalls.Add(temp);
                temp.transform.parent = transform.root.Find("Shelfs").Find("BallsLevel3New");
                temp.SetActive(true);
                lastInstantiateTime = Time.time;
                break;
        }
    }

    internal void SetFirstBalls()
    {
        int lastOpenCollectionIndex = 1;
        if (myCollectionManager != null)
            lastOpenCollectionIndex = myCollectionManager.lastOpenCollectionIndex;
        switch (level)
        {
            case 0:
                rafBalls.Clear();
                transform.root.Find("Shelfs").Find("ShelfLvl1").gameObject.SetActive(true);
                for (int i = 0; i < 14; i++)
                {
                    GameObject temp  = Instantiate(balls[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Shelfs").Find("BallsLevel1").GetChild(13 - i).transform.position,
                        Quaternion.identity, transform.root.Find("Shelfs").Find("BallsLevel1New"));
                    rafBalls.Add(temp);
                    temp.SetActive(true);
                }
                break;
            case 1:
                rafBalls.Clear();
                transform.root.Find("Shelfs").Find("ShelfLvl1").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("ShelfsLvl2").gameObject.SetActive(true);
                for (int i = 0; i < 24; i++)
                {
                    GameObject temp = Instantiate(balls[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Shelfs").Find("BallsLevel2").GetChild(23 - i).transform.position,
                        Quaternion.identity, transform.root.Find("Shelfs").Find("BallsLevel2New"));
                    rafBalls.Add(temp);
                    temp.SetActive(true);
                }
                break;
            case 2:
                rafBalls.Clear();
                transform.root.Find("Shelfs").Find("ShelfLvl1").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("ShelfsLvl2").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("ShelfLvl3").gameObject.SetActive(true);
                for (int i = 0; i < 32; i++)
                {
                    GameObject temp = Instantiate(balls[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Shelfs").Find("BallsLevel3").GetChild(31 - i).transform.position,
                        Quaternion.identity, transform.root.Find("Shelfs").Find("BallsLevel3New"));
                    rafBalls.Add(temp);
                    temp.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void GetBall(bool characterAI = false, AICharacter ai = null)
    {
        if (rafBalls.Count <= 0)
            return;
        if (!characterAI)
        {
            GameObject temp = rafBalls[rafBalls.Count - 1];
            rafBalls.Remove(temp);
            temp.transform.parent = ballCollector.ballStackPoint.transform;
            temp.transform.DOLocalMove(new Vector3(0, ballCollector.ballStackPoint.transform.position.y + (7.6f * ballCollector.stackingBallList.Count), 0), 0.25f);
            temp.transform.DORotate(new Vector3(240, 90, 0), 0.25f);
            StartCoroutine(SetFalse(temp.transform));
            ballCollector.stackingBallList.Add(temp);
            temp.AddComponent<BallMovement>();
            temp.GetComponent<BallMovement>().index = ballCollector.stackingBallList.Count;
            temp.GetComponent<BallMovement>().followedCube = ballCollector.ballStackPoint.transform;
            Vibrations.Medium();
        }
        else
        {
            if(ai == null && aiCharacter == null)
            {
                aiCharacter = FindObjectOfType<AICharacter>();
            }else
            {
                aiCharacter = ai;
            }
            GameObject temp = rafBalls[rafBalls.Count - 1];
            if (temp == null) return;
            rafBalls.Remove(temp);
            temp.transform.parent = aiCharacter.ballStackPoint.transform;
            temp.transform.DOLocalMove(new Vector3(0, aiCharacter.ballStackPoint.transform.position.y + (7.6f * aiCharacter.stackingBallList.Count), 0), 0.25f);
            temp.transform.DORotate(new Vector3(240, 90, 0), 0.25f);
            StartCoroutine(SetFalse(temp.transform));
            aiCharacter.stackingBallList.Add(temp);
            temp.AddComponent<BallMovement>();
            temp.GetComponent<BallMovement>().index = aiCharacter.stackingBallList.Count;
            temp.GetComponent<BallMovement>().followedCube = aiCharacter.ballStackPoint.transform;
        }
    }

    IEnumerator SetFalse(Transform ball)
    {
        yield return new WaitForSeconds(0.3f);
        ball.parent = null;
    }
}