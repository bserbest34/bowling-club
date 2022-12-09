using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI.ProceduralImage;
using System.Collections;

public class BallCollector : MonoBehaviour
{
    JoystickControl jControl;
    AreaManager areaManager;
    CleanArea cleanArea;
    UpgradeSystemManager ugradeCharManager;
    HireEmployeSystemManager hireEmployeManager;

    internal List<GameObject> stackingBallList = new List<GameObject>();
    public int maxBallCount = 3;

    internal List<GameObject> stackingShoesList = new List<GameObject>();
    public int maxShoesCount = 3;

    float lastCollectTime;
    public GameObject moneyInstiateSystem;
    internal bool lastOnotherTime;
    public GameObject ballStackPoint;
    public GameObject shoesStackPoint;
    internal int saloonCapasity;

    GameObject fullText;
    public GameObject officeRoomCam;
    public GameObject mainCam, shoesCleanAreaCam;

    public GameObject receptionMonies;
    public List<BoxCollider> colliders = new List<BoxCollider>();
    public List<GameObject> canvas = new List<GameObject>();
    public DailyTaskManager dailyTaskManager;
    public Transform floatingJoysticTransform;
    public Transform dailyTask;
    public Transform myCollection;
    public Transform vip;

    private void Awake()
    {
        dailyTaskManager = FindObjectOfType<DailyTaskManager>();
    }

    void Start()
    {
        dailyTaskManager = FindObjectOfType<DailyTaskManager>();
        PlayerPrefs.SetInt("ColliderOpen", 0);
        maxBallCount = PlayerPrefs.GetInt("Stack", 3);
        ugradeCharManager = UIManager.Instance.transform.Find("UpgradeChar").GetComponent<UpgradeSystemManager>();
        hireEmployeManager = UIManager.Instance.transform.Find("HireEmploye").GetComponent<HireEmployeSystemManager>();
        cleanArea = FindObjectOfType<CleanArea>();

        jControl = GetComponent<JoystickControl>();
        areaManager = FindObjectOfType<AreaManager>();
        fullText = GameObject.Find("FullText");
        fullText.SetActive(false);
    }

    private void Update()
    {
        SetSallonCapacity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Ball))
        {
            if (stackingBallList.Count >= maxBallCount)
                return;
            other.enabled = false;
            SetBallPosition(other);
        }
        if (other.CompareTag(Tags.BallCollector))
        {
            Vibrations.Succes();
        }

        switch (other.tag) 
        { 
            case Tags.ShelfUpgradeArea:
                Vibrations.Light();
                other.transform.parent.Find("ShelfUpgradeOverlayCanvas").Find("UpgradeButtons").GetComponent<ShelfUpgradeManager>().OpenButtons();
                break;
            case Tags.ShoesUpgradeArea:
                Vibrations.Light();
                other.transform.parent.Find("ShelfUpgradeOverlayCanvas").Find("UpgradeButtons").GetComponent<ShoesAreaUpgrade>().OpenButtons();
                break;
            case Tags.HoverBoard:
                if (jControl.isOnHoverBoard)
                    return;
                Vibrations.Light();
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                break;
            case Tags.MoneyBag:
                Vibrations.Light();
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                break;
            case Tags.Robots:
                Vibrations.Light();
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                break;
            case Tags.BallCollectorAIs:
                Debug.Log("s");
                Vibrations.Light();
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case Tags.Upgrade2:
                Color aColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                aColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = aColor;
                break;
            case Tags.OfficeArea:
                Color cColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                cColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = cColor;
                break;
            case Tags.RafBallArea:
                if(Time.fixedTime - lastCollectTime > 0.3f && stackingBallList.Count <= maxBallCount)
                {
                    if (stackingBallList.Count >= maxBallCount)
                        fullText.SetActive(true);
                    else
                    {
                        other.GetComponent<BallDistributorManager>().GetBall();
                        Vibrations.Medium();
                        lastCollectTime = Time.fixedTime;
                    }
                }
                break;
            case Tags.CollectShoes:
                if (Time.fixedTime - lastCollectTime > 0.3f && stackingShoesList.Count <= maxShoesCount)
                {
                    if (stackingShoesList.Count >= maxShoesCount)
                        fullText.SetActive(true);
                    else
                    {
                        other.GetComponent<ShoesDistrubutor>().GetShoes();
                        Vibrations.Medium();
                        lastCollectTime = Time.fixedTime;
                    }
                }
                break;
            case Tags.Billard:
                Color dColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                dColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = dColor;
                break;
            case Tags.Langert:
                Color fColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                fColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = fColor;
                break;
            case Tags.CleaningArea:
                Color gColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                gColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = gColor;
                break;
            case Tags.NewBallArea:
                Color xColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                xColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = xColor;
                break;
            case Tags.UpgradeCanvas:
                vip.SetSiblingIndex(6);
                dailyTask.SetSiblingIndex(7);
                myCollection.SetSiblingIndex(8);
                other.transform.SetSiblingIndex(12);
                floatingJoysticTransform.SetAsLastSibling();
                ugradeCharManager.OpenButtons();
                officeRoomCam.SetActive(true);
                break;
            case Tags.BuyCustomer:
                vip.SetSiblingIndex(6);
                dailyTask.SetSiblingIndex(7);
                myCollection.SetSiblingIndex(8);
                other.transform.SetSiblingIndex(12);
                floatingJoysticTransform.SetAsLastSibling();
                hireEmployeManager.OpenButtons();
                break;
            case Tags.NewAreaManager:
                other.transform.Find("NewAreaCanvas").Find("UpgradeButtons").GetComponent<NewAreaManager>().OpenButtons();
                break;
            case Tags.OfficeDoor:
                other.transform.Find("Door").GetComponent<Animator>().SetBool("isOpen", true);
                break;
            case Tags.BowlingUpgradeArea:
                other.transform.root.Find("BowlingCanvas").Find("UpgradeButtons").GetComponent<BowlingAreaManager>().OpenButtons();
                break;
            case Tags.NewArea:
                Color bColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                bColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = bColor;
                break;
            case Tags.VIPBowlingArea:
                Color bcColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                bcColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = bcColor;
                break;
            case Tags.TipBox:
                Color bccColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                bccColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = bccColor;
                break;
            case Tags.MoneyTrigger:
                if(Time.time - lastCollectTime > 0.01f)
                {
                    other.transform.parent.GetComponent<Moneys>().GetMoney();
                    lastCollectTime = Time.time;
                }
                break;
        }

        if (!jControl.isRelase) return;

        switch (other.tag)
        {
            case Tags.UpgradeArea:
                if (lastOnotherTime)
                    return;
                other.GetComponent<RafUpgrade>().SetMoneyToUpgradeArea();
                lastOnotherTime = true;
                break;
            case Tags.StackUpgrade:
                if (lastOnotherTime)
                    return;
                other.GetComponent<UpgradeArea>().SetMoneyToUpgradeArea();
                Vibrations.Succes();
                lastOnotherTime = true;
                break;
            case Tags.CleanerAI:
                lastOnotherTime = true;
                other.GetComponent<BuyNewArea>().SetMoneyNewArea();
                break;
            case Tags.OfficeArea:
                other.GetComponent<OfficeArea>().SetNewCustomer();
                break;
            case Tags.DropShoes:
                if (stackingShoesList.Count <= 0 || Time.time - lastCollectTime < 0.1f)
                    break;

                Color cDolor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                cDolor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = cDolor;

                DropTheShoes(other, stackingShoesList[stackingShoesList.Count - 1]);
                break;
            case Tags.BallCollector:
                if (stackingBallList.Count <= 0 || Time.time - lastCollectTime < 0.1f)
                    break;

                Color cColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                cColor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = cColor;

                DropTheBall(other, stackingBallList[stackingBallList.Count - 1]);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case Tags.Upgrade2:
                lastOnotherTime = false;
                Color aColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                aColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = aColor;
                break;
            case Tags.NewArea:
                lastOnotherTime = false;
                Color bColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                bColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = bColor;
                break;
            case Tags.VIPBowlingArea:
                lastOnotherTime = false;
                Color bcColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                bcColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = bcColor;
                break;
            case Tags.TipBox:
                lastOnotherTime = false;
                Color bccColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                bccColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = bccColor;
                break;
            case Tags.OfficeArea:
                Color cColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                cColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = cColor;
                break;
            case Tags.BallCollector:
                Color dColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                dColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = dColor;
                break;
            case Tags.DropShoes:
                Color dcColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                dcColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = dcColor;
                break;
            case Tags.Billard:
                Color fColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                fColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = fColor;
                break;
            case Tags.Langert:
                Color eColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                eColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = eColor;
                break;
            case Tags.NewBallArea:
                Color xColor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                xColor.a = 100f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = xColor;
                break;
            case Tags.RafBallArea:
                fullText.SetActive(false);
                break;
            case Tags.CollectShoes:
                fullText.SetActive(false);
                break;
            case Tags.UpgradeCanvas:
                dailyTask.SetSiblingIndex(13);
                myCollection.SetSiblingIndex(14);
                floatingJoysticTransform.SetSiblingIndex(6);
                ugradeCharManager.CloseButtons();

                officeRoomCam.SetActive(false);
                break;
            case Tags.BuyCustomer:
                dailyTask.SetSiblingIndex(13);
                myCollection.SetSiblingIndex(14);
                floatingJoysticTransform.SetSiblingIndex(6);
                hireEmployeManager.CloseButtons();
                break;
            case Tags.NewAreaManager:
                other.transform.Find("NewAreaCanvas").Find("UpgradeButtons").GetComponent<NewAreaManager>().CloseButtons();
                break;
            case Tags.ShelfUpgradeArea:
                other.transform.parent.Find("ShelfUpgradeOverlayCanvas").Find("UpgradeButtons").GetComponent<ShelfUpgradeManager>().CloseButtons();
                break;
            case Tags.ShoesUpgradeArea:
                other.transform.parent.Find("ShelfUpgradeOverlayCanvas").Find("UpgradeButtons").GetComponent<ShoesAreaUpgrade>().CloseButtons();
                break;
            case Tags.BowlingUpgradeArea:
                other.transform.root.Find("BowlingCanvas").Find("UpgradeButtons").GetComponent<BowlingAreaManager>().CloseButtons();
                break;
            case Tags.OfficeDoor:
                other.transform.Find("Door").GetComponent<Animator>().SetBool("isOpen", false);
                break;
            case Tags.HoverBoard:
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
                break;
            case Tags.MoneyBag:
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
                break;
            case Tags.Robots:
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
                break;
            case Tags.BallCollectorAIs:
                Debug.Log("se");
                other.transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
                break;
        }
    }

    void DropTheBall(Collider other, GameObject ball)
    {
        if (other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.Count == 6)
            return;
        if(dailyTaskManager != null)
            dailyTaskManager.SetValue(Missions.distrubBall, 1);
        ball.GetComponent<BallMovement>().enabled = false;
        lastCollectTime = Time.time;
        stackingBallList.Remove(ball);
        ball.transform.parent = other.transform.parent.Find("RefBall").transform;
        ball.transform.DOMove(new Vector3(ball.transform.parent.position.x, ball.transform.parent.position.y + 0.4f,
            ball.transform.parent.position.z + other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().posZ), 0.4f);
        other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().balls.Add(ball);
        other.transform.parent.Find("RefBall").GetComponent<BallCollectorManager>().posZ -= 1.2f;
        Vibrations.Succes();
    }

    void DropTheShoes(Collider other, GameObject shoes)
    {
        if (other.GetComponent<DropShoes>().shoes.Count == 20)
            return;
        lastCollectTime = Time.time;
        stackingShoesList.Remove(shoes);
        shoes.transform.DOMove(other.transform.Find("ShoesPoint").GetChild(other.GetComponent<DropShoes>().shoes.Count).transform.position, 0.4f);
        shoes.transform.DORotate(other.transform.Find("ShoesPoint").GetChild(other.GetComponent<DropShoes>().shoes.Count).transform.eulerAngles, 0.3f);
        StartCoroutine(SetTrue(other, other.GetComponent<DropShoes>().shoes.Count, shoes));
        other.GetComponent<DropShoes>().shoes.Add(other.transform.Find("ShoesPoint").GetChild(other.GetComponent<DropShoes>().shoes.Count).gameObject);
        Vibrations.Succes();
    }

    IEnumerator SetTrue(Collider other, int index, GameObject shoes)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(shoes);
        other.transform.Find("ShoesPoint").GetChild(index).gameObject.SetActive(true);
    }

    void SetBallPosition(Collider other)
    {
        if (stackingBallList.Count >= maxBallCount)
            return;
        other.enabled = false;
        other.gameObject.transform.parent = ballStackPoint.transform;
        stackingBallList.Add(other.gameObject);
        other.transform.DOLocalRotate(new Vector3(240, 90, 0), 0.25f);
        other.transform.DOLocalJump(new Vector3(0,
            ballStackPoint.transform.position.y + (7.5f * stackingBallList.Count), 0), 10, 1, 0.25f);
    }

    void SetSallonCapacity()
    {
        saloonCapasity = (areaManager.langertAreas.Count * 2) + (areaManager.billardAreas.Count * 2) +
            (areaManager.cafeAreas.Count * 4) + (areaManager.dartAreas.Count) + (areaManager.arcadeMachineArea.Count) +
            (areaManager.miniCafeArea.Count * 3) + (areaManager.tableTennisAreas.Count * 2);

        int temp = 0;
        for (int i = 0; i < areaManager.bowlingAreas.Count; i++)
        {
            temp += areaManager.bowlingAreas[i].GetComponent<BowlingArea>().maxCount;
        }
        saloonCapasity += temp;
    }

    internal void InstantiateMoney()
    {
        var temp = Instantiate(moneyInstiateSystem, transform.position, Quaternion.identity, null);
        temp.SetActive(true);
    }

    internal void StackUpgrade()
    {
        PlayerPrefs.SetInt("Stack", maxBallCount + 1);
        maxBallCount = PlayerPrefs.GetInt("Stack");
    }
}