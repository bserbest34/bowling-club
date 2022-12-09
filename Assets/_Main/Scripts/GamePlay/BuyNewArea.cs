using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI.ProceduralImage;

public class BuyNewArea : MonoBehaviour
{
    AreaManager areaManager;
    AICharacter AICharacter;
    CleanArea cleanAreaScript;

    public float needMoneyCount;
    ProceduralImage fillImage;
    TextMeshProUGUI textMP;
    bool isOpenProcess = false;
    internal int isBought = 0;
    Color mainColor;

    public Transform cleanArea , customerArea;
    private void Awake()
    {
        isBought = PlayerPrefs.GetInt("IsBought" + transform.name);
    }
    void Start()
    {
        cleanAreaScript = FindObjectOfType<CleanArea>();
        areaManager = FindObjectOfType<AreaManager>();
        AICharacter = FindObjectOfType<AICharacter>();
       
        if (isBought == 1)
        {
            transform.Find("Canvas").gameObject.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;

            switch (tag)
            {
                case Tags.NewArea:
                    transform.Find("UnluckBowlingArea").gameObject.SetActive(false);
                    transform.Find("BowlingArea").gameObject.SetActive(true);
                    transform.Find("Pins").gameObject.SetActive(true);
                    transform.Find("BallCollector").gameObject.SetActive(true);
                    transform.Find("AITrigger").gameObject.SetActive(true);
                    transform.Find("BallTrigger").gameObject.SetActive(true);
                    transform.Find("Wall").gameObject.SetActive(true);
                    transform.Find("Ground").gameObject.SetActive(true);
                    transform.Find("Cube").gameObject.SetActive(true);
                    areaManager.bowlingAreas.Add(transform.gameObject);
                    if (areaManager.CollectorAI.gameObject.activeInHierarchy)
                    {
                        AICharacter.OpenedNewBowlingArea(transform.gameObject);
                    }
                    break;
                case Tags.Langert:
                    transform.gameObject.SetActive(true);
                    transform.Find("Foosball").gameObject.SetActive(true);
                    transform.Find("Foosball.001").gameObject.SetActive(true);
                    transform.Find("Foosball.002").gameObject.SetActive(true);
                    transform.Find("Foosball.003").gameObject.SetActive(true);
                    transform.Find("Foosball.004").gameObject.SetActive(true);
                    transform.Find("Foosball.005").gameObject.SetActive(true);
                    transform.Find("Foosball.006").gameObject.SetActive(true);
                    transform.Find("Foosball.007").gameObject.SetActive(true);
                    transform.Find("Foosball.008").gameObject.SetActive(true);
                    transform.Find("Point1").gameObject.SetActive(true);
                    transform.Find("Point1").gameObject.SetActive(true);
                    transform.Find("Time").gameObject.SetActive(true);
                    transform.Find("LookAt").gameObject.SetActive(true);
                    transform.Find("Collider").gameObject.SetActive(true);
                    transform.Find("Monies").gameObject.SetActive(true);
                    GetComponent<NavMeshObstacle>().enabled = true;
                    areaManager.langertAreas.Add(transform.gameObject);
                    break;
                case Tags.Billard:
                    transform.Find("BilliardsBals").gameObject.SetActive(true);
                    transform.Find("Sticks").gameObject.SetActive(true);
                    transform.Find("BilardoTable").gameObject.SetActive(true);
                    transform.Find("BilardoTableUnder").gameObject.SetActive(true);
                    transform.Find("Cube").gameObject.SetActive(true);
                    transform.Find("BillardStickPoint").gameObject.SetActive(true);
                    transform.Find("BillardStickPoint2").gameObject.SetActive(true);
                    transform.Find("Timer").gameObject.SetActive(true);
                    transform.Find("Monies").gameObject.SetActive(true);
                    GetComponent<NavMeshObstacle>().enabled = true;
                    areaManager.billardAreas.Add(transform.gameObject);
                    break;
                case Tags.CleaningArea:
                    transform.Find("Ground").gameObject.SetActive(true);
                    transform.Find("CleaningBottle").gameObject.SetActive(true);
                    transform.Find("ShoesShelfs").gameObject.SetActive(true);
                    transform.Find("Shoes").gameObject.SetActive(true);
                    transform.Find("BarTable").gameObject.SetActive(true);
                    transform.Find("Point").gameObject.SetActive(true);
                    transform.Find("BarTable2").gameObject.SetActive(true);
                    transform.Find("Wall3 (2)").gameObject.SetActive(true);
                    transform.Find("UnluckCleaningArea").gameObject.SetActive(false);
                    GameObject.Find("CleanerAI").transform.Find("Canvas").gameObject.SetActive(true);
                    GameObject.Find("CleanerAI").transform.Find("Cleaner").gameObject.SetActive(true);
                    GameObject.Find("CleanerAI").transform.GetComponent<BoxCollider>().enabled = true;
                    GameObject.Find("OfficeAI").transform.Find("Canvas").gameObject.SetActive(true);
                    GameObject.Find("OfficeAI").transform.Find("AIOfficeCharacter").gameObject.SetActive(true);
                    GameObject.Find("OfficeAI").transform.GetComponent<BoxCollider>().enabled = true;
                    areaManager.cleanArea.Add(transform.gameObject);
                    break;
                case Tags.CafeArea:
                    if (transform.Find("CafeGround") != null)
                        transform.Find("CafeGround").gameObject.SetActive(true);
                    if (transform.Find("Cafe (1)") != null)
                        transform.Find("Cafe (1)").gameObject.SetActive(true);
                    if (transform.Find("Cafe") != null)
                        transform.Find("Cafe").gameObject.SetActive(true);
                    if (transform.Find("CoffeMachine") != null)
                        transform.Find("CoffeMachine").gameObject.SetActive(true);
                    if (transform.Find("CocaMachine") != null)
                        transform.Find("CocaMachine").gameObject.SetActive(true);
                    if (transform.Find("CoffeTableNew (1)") != null)
                        transform.Find("CoffeTableNew (1)").gameObject.SetActive(true);
                    if (transform.Find("CoffeTableNew (2)") != null)
                        transform.Find("CoffeTableNew (2)").gameObject.SetActive(true);
                    if (transform.Find("CoffeTableNew") != null)
                        transform.Find("CoffeTableNew").gameObject.SetActive(true);
                    if (transform.Find("BarChair (1)") != null)
                        transform.Find("BarChair (1)").gameObject.SetActive(true);
                    if (transform.Find("BarChair (2)") != null)
                        transform.Find("BarChair (2)").gameObject.SetActive(true);
                    if (transform.Find("BarChair") != null)
                        transform.Find("BarChair").gameObject.SetActive(true);
                    if (transform.Find("BarTable2") != null)
                        transform.Find("BarTable2").gameObject.SetActive(true);
                    if (transform.Find("BarWorker") != null)
                        transform.Find("BarWorker").gameObject.SetActive(true);
                    areaManager.cafeAreas.Add(transform.gameObject);
                    break;
                case Tags.AICollector:
                    transform.GetComponent<AICharacter>().enabled = true;
                    transform.GetComponent<NavMeshAgent>().enabled = true;
                    transform.Find("ChatBubble").gameObject.SetActive(false);
                    transform.Find("Character").gameObject.SetActive(true);
                    Destroy(transform.GetComponent<Rigidbody>());
                    break;
                case Tags.AIOfficer:
                    Destroy(transform.Find("Canvas").gameObject);
                    transform.Find("AIOfficeCharacter").GetComponent<AIOfficeCharacter>().enabled = true;
                    transform.Find("AIOfficeCharacter").Find("ChatBubble").gameObject.SetActive(false);
                    transform.Find("AIOfficeCharacter").GetComponent<NavMeshAgent>().enabled = true;
                    transform.Find("AIOfficeCharacter").gameObject.layer = LayerMask.NameToLayer("Dynamic");
                    transform.Find("AIOfficeCharacter").GetComponent<BoxCollider>().enabled = true;
                    break;
                case Tags.CleanerAI:
                    Destroy(transform.Find("Canvas").gameObject);
                    transform.Find("Cleaner").GetComponent<AICleaner>().enabled = true;
                    transform.Find("Cleaner").Find("ChatBubble").gameObject.SetActive(false);
                    transform.Find("Cleaner").GetComponent<NavMeshAgent>().enabled = true;
                    transform.Find("Cleaner").gameObject.layer = LayerMask.NameToLayer("Dynamic");
                    transform.Find("Cleaner").GetComponent<BoxCollider>().enabled = true;
                    break;
                case Tags.NewBallArea:
                    for (int i = 0; i < transform.Find("BallsLevel1").childCount; i++)
                    {
                        transform.GetComponent<BallDistributorManager>().rafBalls.Add(transform.Find("BallsLevel1").GetChild(i).gameObject);
                    }
                    transform.Find("BallsLevel1").gameObject.SetActive(true);
                    transform.Find("ShelfLvl1").gameObject.SetActive(true);
                    transform.Find("UpgradeProductionTime").gameObject.SetActive(true);
                    transform.Find("Canvas").Find("Image").gameObject.SetActive(false);
                    transform.Find("Canvas").Find("TextMP").gameObject.SetActive(false);
                    transform.Find("Canvas").gameObject.SetActive(true);
                    transform.GetComponent<BoxCollider>().enabled = true;
                    transform.tag = Tags.RafBallArea;
                    break;
            }
        }
        fillImage = transform.Find("Canvas").Find("Image").GetComponent<ProceduralImage>();
        textMP = transform.Find("Canvas").Find("TextMP").GetComponent<TextMeshProUGUI>();
        textMP.text = needMoneyCount + "$";
        mainColor = transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
    }

    internal void SetMoneyNewArea()
    {
        if (isOpenProcess)
            return;

        isOpenProcess = true;
        if (MoneyManager.Instance.money >= needMoneyCount)
        {
            MoneyManager.Instance.IncreaseMoneyAndWrite(-needMoneyCount);
            FindObjectOfType<BallCollector>().InstantiateMoney();
            StartCoroutine(SetFill());
        }
        else
        {
            transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = Color.red;
            StartCoroutine(SetFalse());
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(1f);
        transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = mainColor;
        isOpenProcess = false;
    }

    IEnumerator SetFill()
    {
        float velocity = 0f;
        while (fillImage.fillAmount < 1)
        {
            yield return new WaitForSeconds(0);
            fillImage.fillAmount = Mathf.SmoothDamp(fillImage.fillAmount, 1, ref velocity, Time.deltaTime * 0.1f, 10);
        }

        transform.Find("Canvas").gameObject.SetActive(false);

        Vibrations.Succes();

        isBought = 1;
        PlayerPrefs.SetInt("IsBought" + transform.name, isBought);
        GetComponent<BoxCollider>().enabled = false;

        switch (tag)
        {
            case Tags.NewArea:
                transform.Find("UnluckBowlingArea").gameObject.SetActive(false);
                transform.Find("BowlingArea").gameObject.SetActive(true);
                transform.Find("Pins").gameObject.SetActive(true);
                transform.Find("BallCollector").gameObject.SetActive(true);
                transform.Find("AITrigger").gameObject.SetActive(true);
                transform.Find("BallTrigger").gameObject.SetActive(true);
                transform.Find("Wall").gameObject.SetActive(true);
                transform.Find("Ground").gameObject.SetActive(true);
                transform.Find("Cube").gameObject.SetActive(true);
                areaManager.bowlingAreas.Add(transform.gameObject);
                if (areaManager.CollectorAI.gameObject.activeInHierarchy)
                {
                    AICharacter.OpenedNewBowlingArea(transform.gameObject);
                }
                break;
            case Tags.Langert:
                transform.gameObject.SetActive(true);
                transform.Find("Foosball").gameObject.SetActive(true);
                transform.Find("Foosball.001").gameObject.SetActive(true);
                transform.Find("Foosball.002").gameObject.SetActive(true);
                transform.Find("Foosball.003").gameObject.SetActive(true);
                transform.Find("Foosball.004").gameObject.SetActive(true);
                transform.Find("Foosball.005").gameObject.SetActive(true);
                transform.Find("Foosball.006").gameObject.SetActive(true);
                transform.Find("Foosball.007").gameObject.SetActive(true);
                transform.Find("Foosball.008").gameObject.SetActive(true);
                transform.Find("Point1").gameObject.SetActive(true);
                transform.Find("Point1").gameObject.SetActive(true);
                transform.Find("Time").gameObject.SetActive(true);
                transform.Find("LookAt").gameObject.SetActive(true);
                transform.Find("Collider").gameObject.SetActive(true);
                transform.Find("Monies").gameObject.SetActive(true);
                GetComponent<NavMeshObstacle>().enabled = true;
                areaManager.langertAreas.Add(transform.gameObject);
                break;
            case Tags.Billard:
                transform.Find("BilliardsBals").gameObject.SetActive(true);
                transform.Find("Sticks").gameObject.SetActive(true);
                transform.Find("BilardoTable").gameObject.SetActive(true);
                transform.Find("BilardoTableUnder").gameObject.SetActive(true);
                transform.Find("Cube").gameObject.SetActive(true);
                transform.Find("BillardStickPoint").gameObject.SetActive(true);
                transform.Find("BillardStickPoint2").gameObject.SetActive(true);
                transform.Find("Timer").gameObject.SetActive(true);
                transform.Find("Monies").gameObject.SetActive(true);
                GetComponent<NavMeshObstacle>().enabled = true;
                areaManager.billardAreas.Add(transform.gameObject);
                break;
            case Tags.CleaningArea:
                transform.Find("Ground").gameObject.SetActive(true);
                transform.Find("CleaningBottle").gameObject.SetActive(true);
                transform.Find("ShoesShelfs").gameObject.SetActive(true);
                transform.Find("Shoes").gameObject.SetActive(true);
                transform.Find("BarTable").gameObject.SetActive(true);
                transform.Find("Point").gameObject.SetActive(true);
                transform.Find("BarTable2").gameObject.SetActive(true);
                transform.Find("Wall3 (2)").gameObject.SetActive(true);
                transform.Find("UnluckCleaningArea").gameObject.SetActive(false);
                GameObject.Find("CleanerAI").transform.Find("Canvas").gameObject.SetActive(true);
                GameObject.Find("CleanerAI").transform.Find("Cleaner").gameObject.SetActive(true);
                GameObject.Find("CleanerAI").transform.GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("OfficeAI").transform.Find("Canvas").gameObject.SetActive(true);
                GameObject.Find("OfficeAI").transform.Find("AIOfficeCharacter").gameObject.SetActive(true);
                GameObject.Find("OfficeAI").transform.GetComponent<BoxCollider>().enabled = true;
                areaManager.cleanArea.Add(transform.gameObject);
                break;
            case Tags.CafeArea:
                if (transform.Find("CafeGround") != null)
                    transform.Find("CafeGround").gameObject.SetActive(true);
                if (transform.Find("Cafe (1)") != null)
                    transform.Find("Cafe (1)").gameObject.SetActive(true);
                if (transform.Find("Cafe") != null)
                    transform.Find("Cafe").gameObject.SetActive(true);
                if (transform.Find("CoffeMachine") != null)
                    transform.Find("CoffeMachine").gameObject.SetActive(true);
                if (transform.Find("CocaMachine") != null)
                    transform.Find("CocaMachine").gameObject.SetActive(true);
                if (transform.Find("CoffeTableNew (1)") != null)
                    transform.Find("CoffeTableNew (1)").gameObject.SetActive(true);
                if (transform.Find("CoffeTableNew (2)") != null)
                    transform.Find("CoffeTableNew (2)").gameObject.SetActive(true);
                if (transform.Find("CoffeTableNew") != null)
                    transform.Find("CoffeTableNew").gameObject.SetActive(true);
                if (transform.Find("BarChair (1)") != null)
                    transform.Find("BarChair (1)").gameObject.SetActive(true);
                if (transform.Find("BarChair (2)") != null)
                    transform.Find("BarChair (2)").gameObject.SetActive(true);
                if (transform.Find("BarChair") != null)
                    transform.Find("BarChair").gameObject.SetActive(true);
                if (transform.Find("BarTable2") != null)
                    transform.Find("BarTable2").gameObject.SetActive(true);
                if (transform.Find("BarWorker") != null)
                    transform.Find("BarWorker").gameObject.SetActive(true);
                areaManager.cafeAreas.Add(transform.gameObject);
                break;
            case Tags.AICollector:
                transform.GetComponent<AICharacter>().enabled = true;
                transform.GetComponent<NavMeshAgent>().enabled = true;
                transform.Find("ChatBubble").gameObject.SetActive(false);
                Destroy(transform.GetComponent<Rigidbody>());
                break;
            case Tags.AIOfficer:
                Destroy(transform.Find("Canvas").gameObject);
                transform.Find("AIOfficeCharacter").GetComponent<AIOfficeCharacter>().enabled = true;
                transform.Find("AIOfficeCharacter").Find("ChatBubble").gameObject.SetActive(false);
                transform.Find("AIOfficeCharacter").GetComponent<NavMeshAgent>().enabled = true;
                transform.Find("AIOfficeCharacter").gameObject.layer = LayerMask.NameToLayer("Dynamic");
                transform.Find("AIOfficeCharacter").GetComponent<BoxCollider>().enabled = true;
                break;
            case Tags.CleanerAI:
                transform.Find("Cleaner").GetComponent<AICleaner>().enabled = true;
                transform.Find("Cleaner").Find("ChatBubble").gameObject.SetActive(false);
                transform.Find("Cleaner").GetComponent<NavMeshAgent>().enabled = true;
                transform.Find("Cleaner").gameObject.layer = LayerMask.NameToLayer("Dynamic");
                transform.Find("Cleaner").GetComponent<BoxCollider>().enabled = true;
                break;
            case Tags.NewBallArea:
                for (int i = 0; i < transform.Find("BallsLevel1").childCount; i++)
                {
                    transform.GetComponent<BallDistributorManager>().rafBalls.Add(transform.Find("BallsLevel1").GetChild(i).gameObject);
                }
                transform.Find("BallsLevel1").gameObject.SetActive(true);
                transform.Find("ShelfLvl1").gameObject.SetActive(true);
                transform.Find("UpgradeProductionTime").gameObject.SetActive(true);
                transform.Find("Canvas").Find("Image").gameObject.SetActive(false);
                transform.Find("Canvas").Find("TextMP").gameObject.SetActive(false);
                transform.Find("Canvas").gameObject.SetActive(true);
                transform.GetComponent<BoxCollider>().enabled = true;
                transform.tag = Tags.RafBallArea;
                break;
        }
        isOpenProcess = false;
    }
}
