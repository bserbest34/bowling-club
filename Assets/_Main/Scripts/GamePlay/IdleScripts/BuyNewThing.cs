using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;
using GameAnalyticsSDK;

public class BuyNewThing : MonoBehaviour
{
    AreaManager areaManager;
    AICharacter AICharacter;
    public Transform cleanArea, customerArea;

    public float cost = 20;
    internal float openPart = 0;

    Image image;
    TextMeshProUGUI text;
    float time = 0;

    public List<GameObject> unlockedObjects = new List<GameObject>();
    public GameObject openableArea3;
    public GameObject cam;

    internal DailyTaskManager dailyTaskManager;
    bool isVip = false;
    JoystickControl jControl;

    private void SaveSystem()
    {
        PlayerPrefs.SetFloat("IsBought" + transform.name, openPart);
    }

    private void Awake()
    {
        jControl = FindObjectOfType<JoystickControl>();
        if(name == "OpenableArea3")
        {
            if(PlayerPrefs.GetFloat("IsBought" + "OpenableArea2") < 5000)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        openPart = PlayerPrefs.GetFloat("IsBought" + transform.name);
        if (PlayerPrefs.GetInt(Key.VipLangert + transform.root.name, 0) == 0)
        {
            isVip = false;
        }
        else
        {
            isVip = true;
        }
    }

    void Start()
    {
        dailyTaskManager = FindObjectOfType<DailyTaskManager>();
        areaManager = FindObjectOfType<AreaManager>();
        AICharacter = FindObjectOfType<AICharacter>();

        image = transform.Find("Canvas").Find("Image").GetComponent<Image>();
        text = transform.Find("Canvas").Find("TextMP").GetComponent<TextMeshProUGUI>();
        
        image.fillAmount = openPart * (1 / cost);
        text.text = cost - openPart + "$";

        if (openPart >= cost)
        {
            transform.Find("Canvas").gameObject.SetActive(false);
            switch (tag)
            {
                case Tags.NewArea:
                    transform.GetComponent<BoxCollider>().enabled = false;
                    PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + name);
                    transform.GetComponent<BowlingArea>().SetBowlingAreaLevel();
                    transform.Find("UnluckBowlingArea").gameObject.SetActive(false);
                    transform.Find("UpgradeCanvas").gameObject.SetActive(true);
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
                        if (AICharacter == null)
                            FindObjectOfType<AICharacter>();
                        AICharacter.OpenedNewBowlingArea(transform.gameObject);
                    }
                    transform.GetComponent<BowlingArea>().SetBowlingAreaLevel();
                    openPart = cost;
                    break;
                case Tags.CleaningArea:
                    transform.tag = "Untagged";
                    transform.GetComponent<BoxCollider>().enabled = false;
                    transform.Find("Unlock").gameObject.SetActive(true);
                    transform.Find("UnluckCleaningArea").gameObject.SetActive(false);
                    areaManager.cleanArea.Add(transform.gameObject);
                    break;
                case Tags.SecondPart:
                    foreach (var item in unlockedObjects)
                    {
                        if(item.activeInHierarchy)
                        {
                            item.gameObject.SetActive(false);
                        }else
                        {
                            item.gameObject.SetActive(true);
                        }
                        Destroy(item);
                    }
                    Destroy(gameObject);
                    break;
                case Tags.NewDartArea:
                    transform.tag = "Untagged";
                    if (isVip)
                    {
                        transform.Find("UnlockVIP").gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.Find("Unlock").gameObject.SetActive(true);
                    }
                    areaManager.dartAreas.Add(transform.gameObject);
                    break;
                case Tags.ArcadeMachine:
                    transform.tag = "Untagged";
                    if (isVip)
                    {
                        transform.Find("UnlockVIP").gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.Find("Unlock").gameObject.SetActive(true);
                    }
                    GetComponent<NavMeshObstacle>().enabled = true;
                    areaManager.arcadeMachineArea.Add(transform.gameObject);
                    break;
                case Tags.MiniCafeArea:
                    transform.tag = "Untagged";
                    Destroy(transform.GetComponent<BoxCollider>());
                    transform.Find("MiniCafeAreaChilds").gameObject.SetActive(true);
                    transform.Find("MiniLocked").gameObject.SetActive(false);
                    areaManager.miniCafeArea.Add(transform.gameObject);
                    break;
                case Tags.Billard:
                    transform.tag = "Untagged";
                    if (isVip)
                    {
                        transform.Find("UnlockVIP").gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.Find("Unlock").gameObject.SetActive(true);
                    }
                    transform.Find("Sticks").gameObject.SetActive(true);
                    GetComponent<NavMeshObstacle>().enabled = true;
                    areaManager.billardAreas.Add(transform.gameObject);
                    break;
                case Tags.Langert:
                    transform.tag = "Untagged";
                    if(isVip)
                    {
                        transform.Find("UnlockVIP").gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.Find("Unlock").gameObject.SetActive(true);
                    }
                    GetComponent<NavMeshObstacle>().enabled = true;
                    areaManager.langertAreas.Add(transform.gameObject);
                    break;
                case Tags.CafeArea:
                    transform.tag = "Untagged";
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
                case Tags.TableTennis:
                    transform.tag = "Untagged";
                    if (isVip)
                    {
                        transform.Find("UnlockVIP").gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.Find("Unlock").gameObject.SetActive(true);
                    }
                    GetComponent<NavMeshObstacle>().enabled = true;
                    areaManager.tableTennisAreas.Add(transform.gameObject);
                    break;
                case Tags.Shelf:
                    GetComponent<BoxCollider>().enabled = false;
                    transform.Find("Canvas").gameObject.SetActive(false);
                    transform.Find("Locked").gameObject.SetActive(false);
                    transform.Find("Shelfs").gameObject.SetActive(true); 
                    break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && jControl.isRelase)
        {
            if (dailyTaskManager == null && FindObjectOfType<DailyTaskManager>() != null)
                dailyTaskManager = FindObjectOfType<DailyTaskManager>();
            if(Time.time - time > 0.05)
            {
                if (MoneyManager.Instance.money <= 0)
                {
                    transform.Find("Canvas").Find("NotEnough").gameObject.SetActive(true);
                    transform.Find("Canvas").Find("TextMP").transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
                    return;
                }

                if(MoneyManager.Instance.money > (int)cost / 20 && cost - openPart >= (int)cost / 20)
                {
                    MoneyManager.Instance.IncreaseMoneyAndWrite(-(int)cost / 20);
                    openPart += (int)cost / 20;
                }else
                {
                    if(cost - openPart > MoneyManager.Instance.money)
                    {
                        var m = MoneyManager.Instance.money;
                        MoneyManager.Instance.IncreaseMoneyAndWrite(-m);
                        openPart += m;
                    }else
                    {
                        var m = cost - openPart;
                        MoneyManager.Instance.IncreaseMoneyAndWrite(-m);
                        openPart += m;
                    }
                }
                image.fillAmount = openPart * (1 / cost);
                text.text = cost - openPart + "$";
                time = Time.time;
                if (openPart >= cost)
                {
                    transform.Find("Canvas").gameObject.SetActive(false);
                    switch (tag)
                    {
                        case Tags.NewArea:
                            if(dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openNewBowlingArea, 1);
                            transform.GetComponent<BoxCollider>().enabled = false;
                            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + name, transform.GetComponent<BowlingArea>().bowlingAreaLevel + 1);
                            transform.GetComponent<BowlingArea>().SetBowlingAreaLevel();
                            transform.Find("UnluckBowlingArea").gameObject.SetActive(false);
                            //transform.Find("UpgradeCanvas").gameObject.SetActive(true);
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
                                if (AICharacter == null)
                                    AICharacter = FindObjectOfType<AICharacter>();
                                AICharacter.OpenedNewBowlingArea(transform.gameObject);
                            }
                            openPart = cost;
                            StartCoroutine(OpenUpgrade());
                            break;

                        case Tags.CleaningArea:
                            transform.tag = "Untagged";
                            transform.GetComponent<BoxCollider>().enabled = false;
                            transform.Find("Unlock").gameObject.SetActive(true);
                            transform.Find("UnluckCleaningArea").gameObject.SetActive(false);
                            areaManager.cleanArea.Add(transform.gameObject);
                            break;
                        case Tags.SecondPart:
                            if(name == "OpenableArea2")
                            {
                                StartCoroutine(SetCamera());
                                openableArea3.SetActive(true);

                                foreach (var item in unlockedObjects)
                                {
                                    if (item == null) break;
                                    if (item.activeInHierarchy)
                                    {
                                        item.gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        item.gameObject.SetActive(true);
                                    }
                                    Destroy(item);
                                }
                                break;
                            }
                            foreach (var item in unlockedObjects)
                            {
                                if (item.activeInHierarchy)
                                {
                                    item.gameObject.SetActive(false);
                                }
                                else
                                {
                                    item.gameObject.SetActive(true);
                                }
                                Destroy(item);
                            }
                            GameAnalytics.NewDesignEvent("Checkpoint:" + transform.name);
                            Destroy(gameObject);
                            break;
                        case Tags.NewDartArea:
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openMinigame, 1);
                            transform.tag = "Untagged";
                            transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                            areaManager.dartAreas.Add(transform.gameObject);
                            break;
                        case Tags.ArcadeMachine:
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openMinigame, 1);
                            transform.tag = "Untagged";
                            transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                            GetComponent<NavMeshObstacle>().enabled = true;
                            areaManager.arcadeMachineArea.Add(transform.gameObject);
                            break;
                        case Tags.MiniCafeArea: // !
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openCafe, 1);
                            transform.tag = "Untagged";
                            Destroy(transform.GetComponent<BoxCollider>());
                            transform.Find("MiniCafeAreaChilds").gameObject.SetActive(true);
                            transform.Find("MiniLocked").gameObject.SetActive(false);
                            areaManager.miniCafeArea.Add(transform.gameObject);
                            break;
                        case Tags.TableTennis:
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openMinigame, 1);
                            transform.tag = "Untagged";
                            transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                            GetComponent<NavMeshObstacle>().enabled = true;
                            areaManager.tableTennisAreas.Add(transform.gameObject);
                            break;
                        case Tags.Billard:
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openMinigame, 1);
                            transform.tag = "Untagged";
                            transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                            GetComponent<NavMeshObstacle>().enabled = true;
                            areaManager.billardAreas.Add(transform.gameObject);
                            break;
                        case Tags.Langert:
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openMinigame, 1);
                            transform.tag = "Untagged";
                            transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(true);
                            GetComponent<NavMeshObstacle>().enabled = true;
                            areaManager.langertAreas.Add(transform.gameObject);
                            break;
                        case Tags.CafeArea: // !
                            if (dailyTaskManager != null)
                                dailyTaskManager.SetValue(Missions.openCafe, 1);
                            transform.tag = "Untagged";
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
                        case Tags.Shelf:
                            GetComponent<BoxCollider>().enabled = false;
                            transform.Find("Canvas").gameObject.SetActive(false);
                            transform.Find("Locked").gameObject.SetActive(false);
                            transform.Find("Shelfs").gameObject.SetActive(true);
                            break;
                    }
                }
                SaveSystem();
            }
        }
    }

    IEnumerator OpenUpgrade()
    {
        yield return new WaitForSeconds(8f);
        transform.Find("UpgradeCanvas").gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.Find("Canvas").Find("NotEnough").gameObject.SetActive(false);
        }
    }

    IEnumerator SetCamera()
    {
        cam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        cam.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}