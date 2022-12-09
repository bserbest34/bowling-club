using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Mission
{
    public Missions mission;
    public float count;
    public float money;
    //public float money;
}

public enum Missions
{
    earnMoney, distrubBall, hireEmploye, openNewBowlingArea, openMinigame, distrubShoes, openCafe
}

public class DailyTaskManager : MonoBehaviour
{
    Image targetImage;
    public List<Sprite> sprites = new List<Sprite>();

    public GameObject excMark;
    public List<Mission> missions = new List<Mission>();
    public Button dailyTaskButton;
    public Button closeTaskButton;
    public Transform task1, task2, task3, completedTask;
    Button collectTask1, collectTask2, collectTask3, collectCompletedTask;
    int lastCompletedMissionIndex = 0;
    float earnMoneyCount, distrubBallCount, hireEmployeCount,
        openBowlingAreaCount, openMiniGameCount, distrubShoesCount,
        openCafeCount, getCustomerToCafeCount;
    bool task1IsCompleted = false, task2IsCompleted = false, task3IsCompleted = false, allTaskCompleted = false;
    MyCollectionManager myCollectionManager;
    float time = 0;

    internal void SetValue(Missions mission, float value)
    {
        switch (mission)
        {
            case Missions.earnMoney:
                earnMoneyCount += value;
                PlayerPrefs.SetFloat("earnMoneyCount", earnMoneyCount);
                break;
            case Missions.distrubBall:
                distrubBallCount += value;
                PlayerPrefs.SetFloat("distrubBallCount", distrubBallCount);
                break;
            case Missions.hireEmploye:
                hireEmployeCount += value;
                PlayerPrefs.SetFloat("hireEmployeCount", hireEmployeCount);
                break;
            case Missions.openNewBowlingArea:
                openBowlingAreaCount += value;
                PlayerPrefs.SetFloat("openBowlingAreaCount", openBowlingAreaCount);
                break;
            case Missions.openMinigame:
                openMiniGameCount += value;
                PlayerPrefs.SetFloat("openMiniGameCount", openMiniGameCount);
                break;
            case Missions.distrubShoes:
                distrubShoesCount += value;
                PlayerPrefs.SetFloat("distrubShoesCount", distrubShoesCount);
                break;
            case Missions.openCafe:
                openCafeCount += value;
                PlayerPrefs.SetFloat("openCafeCount", openCafeCount);
                break;
        }
        SetValues();
    }

    private void Awake()
    {
        myCollectionManager = FindObjectOfType<MyCollectionManager>();
        targetImage = transform.Find("CompleteTask").Find("CollectRewardBtn").Find("RewardImage").GetComponent<Image>();
        if (myCollectionManager != null)
            myCollectionManager.lastOpenCollectionIndex = PlayerPrefs.GetInt("lastOpenCollectionIndex");
        lastCompletedMissionIndex = PlayerPrefs.GetInt("lastCompletedIndex", 0);
        if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone", 0) == 0)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    private void Update()
    {
        if (excMark.activeInHierarchy && Time.time - time > 5f)
        {
            dailyTaskButton.transform.DOShakeRotation(0.5f);
            time = Time.time;
        }
    }

    void Start()
    {
        dailyTaskButton.onClick.AddListener(OpenDailyTask);
        closeTaskButton.onClick.AddListener(CloseDailyTask);

        collectTask1 = task1.transform.Find("CollectRewardBtn").Find("ClaimButton").GetComponent<Button>();
        collectTask2 = task2.transform.Find("CollectRewardBtn").Find("ClaimButton").GetComponent<Button>();
        collectTask3 = task3.transform.Find("CollectRewardBtn").Find("ClaimButton").GetComponent<Button>();
        collectCompletedTask = completedTask.transform.Find("CollectRewardBtn").Find("ClaimButton").GetComponent<Button>();

        collectTask1.onClick.AddListener(CollectTask1);
        collectTask2.onClick.AddListener(CollectTask2);
        collectTask3.onClick.AddListener(CollectTask3);
        collectCompletedTask.onClick.AddListener(CollectCompletedTask);

        SetValues();

        if (sprites.Count > lastCompletedMissionIndex / 3)
            targetImage.sprite = sprites[lastCompletedMissionIndex / 3];
    }

    void SetTask(Transform task)
    {
        task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
        if (task == task1 && task1IsCompleted == false ||
           task == task2 && task2IsCompleted == false ||
           task == task3 && task3IsCompleted == false)
        {
            excMark.SetActive(true);
            task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
            task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(true);
        }else
        {
            task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
            task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARDED";
        }
    }

    void SetMission(Transform task, Missions mission, int missionIndex)
    {
        switch (mission)
        {
            case Missions.earnMoney:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Earn " + missions[missionIndex].count + " Money";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    earnMoneyCount <= missions[missionIndex].count ? earnMoneyCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = earnMoneyCount / missions[missionIndex].count;
                if(earnMoneyCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
            case Missions.distrubBall:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Distrub " + missions[missionIndex].count + " Ball";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    distrubBallCount <= missions[missionIndex].count ? distrubBallCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = distrubBallCount / missions[missionIndex].count;
                if (distrubBallCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }
                else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
            case Missions.hireEmploye:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Hire " + missions[missionIndex].count + " Employe";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    hireEmployeCount <= missions[missionIndex].count ? hireEmployeCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = hireEmployeCount / missions[missionIndex].count;
                if (hireEmployeCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }
                else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
            case Missions.openNewBowlingArea:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Open " + missions[missionIndex].count + " New Bowling Area";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    openBowlingAreaCount <= missions[missionIndex].count ? openBowlingAreaCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = openBowlingAreaCount / missions[missionIndex].count;
                if (openBowlingAreaCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }
                else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
            case Missions.openMinigame:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Open " + missions[missionIndex].count + " Mini Game";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    openMiniGameCount <= missions[missionIndex].count ? openMiniGameCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = openMiniGameCount / missions[missionIndex].count;
                if (openMiniGameCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }
                else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
            case Missions.distrubShoes:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Distrub " + missions[missionIndex].count + " Shoes";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    distrubShoesCount <= missions[missionIndex].count ? distrubShoesCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = distrubShoesCount / missions[missionIndex].count;
                if (distrubShoesCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }
                else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
            case Missions.openCafe:
                task.Find("TaskText").GetComponent<TextMeshProUGUI>().text = "Open " + missions[missionIndex].count + " Cafe";
                task.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text =
                    openCafeCount <= missions[missionIndex].count ? openCafeCount + " / " + missions[missionIndex].count : missions[missionIndex].count + " / " + missions[missionIndex].count;
                task.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = openCafeCount / missions[missionIndex].count;
                if (openCafeCount >= missions[missionIndex].count)
                {
                    SetTask(task);
                }
                else
                {
                    task.transform.Find("CollectRewardBtn").Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
                    task.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
                }
                break;
        }
    }

    void OpenDailyTask()
    {
        Vibrations.Selection();
        transform.SetAsLastSibling();
        SetValues();
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void SetValues()
    {
        earnMoneyCount = PlayerPrefs.GetFloat("earnMoneyCount", 0);
        distrubBallCount = PlayerPrefs.GetFloat("distrubBallCount", 0);
        hireEmployeCount = PlayerPrefs.GetFloat("hireEmployeCount", 0);
        openBowlingAreaCount = PlayerPrefs.GetFloat("openBowlingAreaCount", 0);
        openMiniGameCount = PlayerPrefs.GetFloat("openMiniGameCount", 0);
        distrubShoesCount = PlayerPrefs.GetFloat("distrubShoesCount", 0);
        openCafeCount = PlayerPrefs.GetFloat("openCafeCount", 0);
        getCustomerToCafeCount = PlayerPrefs.GetFloat("getCustomerToCafeCount", 0);
        SetCompletedTask();

        for (int i = lastCompletedMissionIndex; i < lastCompletedMissionIndex + 3; i++)
        {
            int currentMission = i - lastCompletedMissionIndex;
            switch (currentMission)
            {
                case 0:
                    SetMission(task1, missions[i].mission, i);
                    break;
                case 1:
                    SetMission(task2, missions[i].mission, i);
                    break;
                case 2:
                    SetMission(task3, missions[i].mission, i);
                    break;
            }
        }
    }

    void CloseDailyTask()
    {
        Vibrations.Selection();
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void CollectTask1()
    {
        Vibrations.Selection();
        if (myCollectionManager == null)
            myCollectionManager = FindObjectOfType<MyCollectionManager>();
        excMark.SetActive(false);
        collectTask1.gameObject.SetActive(false);
        collectTask1.transform.parent.Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARDED";

        task1IsCompleted = true;
        PlayerPrefs.SetInt("task1IsCompleted", 1);

        MoneyManager.Instance.CreateMoney((int)(missions[lastCompletedMissionIndex].money / MoneyManager.Instance.moneyObjectValue), true, collectTask1.transform.position, true);
        SetCompletedTask();

    }
    void CollectTask2()
    {
        Vibrations.Selection();
        if (myCollectionManager == null)
            myCollectionManager = FindObjectOfType<MyCollectionManager>();
        excMark.SetActive(false);
        collectTask2.gameObject.SetActive(false);
        collectTask2.transform.parent.Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARDED";
        task2IsCompleted = true;
        PlayerPrefs.SetInt("task2IsCompleted", 1);
        MoneyManager.Instance.CreateMoney((int)(missions[lastCompletedMissionIndex + 1].money / MoneyManager.Instance.moneyObjectValue), true, collectTask2.transform.position, true);
        SetCompletedTask();
    }
    void CollectTask3()
    {
        Vibrations.Selection();
        if (myCollectionManager == null)
            myCollectionManager = FindObjectOfType<MyCollectionManager>();
        excMark.SetActive(false);
        collectTask3.gameObject.SetActive(false);
        collectTask3.transform.parent.Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARDED";
        task3IsCompleted = true;
        PlayerPrefs.SetInt("task3IsCompleted", 1);
        MoneyManager.Instance.CreateMoney((int)(missions[lastCompletedMissionIndex + 2].money / MoneyManager.Instance.moneyObjectValue), true, collectTask3.transform.position, true);
        SetCompletedTask();
    }
    void CollectCompletedTask()
    {
        Vibrations.Selection();
        if (myCollectionManager == null)
            myCollectionManager = FindObjectOfType<MyCollectionManager>();
        myCollectionManager.IncreaseValue();
        excMark.SetActive(false);
        collectCompletedTask.gameObject.SetActive(false);
        collectCompletedTask.transform.parent.Find("RewardText").GetComponent<TextMeshProUGUI>().text = "REWARD";
        PlayerPrefs.SetInt("allTaskCompleted", 1);



        lastCompletedMissionIndex += 3;
        if (lastCompletedMissionIndex >= 39)
            lastCompletedMissionIndex = 0;
        PlayerPrefs.SetInt("lastCompletedIndex", lastCompletedMissionIndex);
        if (lastCompletedMissionIndex > missions.Count)
        {
            lastCompletedMissionIndex = 0;
            PlayerPrefs.SetInt("lastCompletedIndex", lastCompletedMissionIndex);
        }

        PlayerPrefs.SetInt("task1IsCompleted", 0);
        PlayerPrefs.SetInt("task2IsCompleted", 0);
        PlayerPrefs.SetInt("task3IsCompleted", 0);
        PlayerPrefs.SetInt("allTaskCompleted", 0);
        earnMoneyCount = 0;
        distrubBallCount = 0;
        hireEmployeCount = 0;
        openBowlingAreaCount = 0;
        openMiniGameCount = 0;
        distrubShoesCount = 0;
        openCafeCount = 0;
        getCustomerToCafeCount = 0;


        PlayerPrefs.SetFloat("earnMoneyCount", earnMoneyCount);
        PlayerPrefs.SetFloat("distrubBallCount", distrubBallCount);
        PlayerPrefs.SetFloat("hireEmployeCount", hireEmployeCount);
        PlayerPrefs.SetFloat("openBowlingAreaCount", openBowlingAreaCount);
        PlayerPrefs.SetFloat("openMiniGameCount", openMiniGameCount);
        PlayerPrefs.SetFloat("distrubShoesCount", distrubShoesCount);
        PlayerPrefs.SetFloat("openCafeCount", openCafeCount);
        PlayerPrefs.SetFloat("getCustomerToCafeCount", getCustomerToCafeCount);

        SetValues();

        if(sprites.Count > lastCompletedMissionIndex / 3)
            targetImage.sprite = sprites[lastCompletedMissionIndex / 3];
    }
    void SetCompletedTask()
    {
        float completedTaskCount = 0;
        if (PlayerPrefs.GetInt("task1IsCompleted", 0) == 0)
        {
            task1IsCompleted = false;
        }
        else
        {
            task1IsCompleted = true;
            completedTaskCount += 1;
        }
        if (PlayerPrefs.GetInt("task2IsCompleted", 0) == 0)
        {
            task2IsCompleted = false;
        }
        else
        {
            task2IsCompleted = true;
            completedTaskCount += 1;
        }
        if (PlayerPrefs.GetInt("task3IsCompleted", 0) == 0)
        {
            task3IsCompleted = false;
        }
        else
        {
            task3IsCompleted = true;
            completedTaskCount += 1;
        }

        if (PlayerPrefs.GetInt("allTaskCompleted", 0) == 0)
        {
            allTaskCompleted = false;
        }
        else
        {
            allTaskCompleted = true;
            
        }

        completedTask.transform.Find("Bar").Find("Text").GetComponent<TextMeshProUGUI>().text = completedTaskCount + " / " + 3;
        completedTask.transform.Find("Bar").Find("FinishBar").GetComponent<Image>().fillAmount = completedTaskCount / 3f;


        if (completedTaskCount < 3)
        {
            completedTask.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
        }
        else
        {
            if (allTaskCompleted == false)
            {
                excMark.SetActive(true);
                completedTask.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(true);
            }else
            {
                completedTask.transform.Find("CollectRewardBtn").Find("ClaimButton").gameObject.SetActive(false);
            }
        }
    }
}