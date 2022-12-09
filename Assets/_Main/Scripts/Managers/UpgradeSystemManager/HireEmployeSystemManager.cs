using UnityEngine;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class HireEmployeSystemManager : HireEmployeSystemBaseManager
{
    DailyTaskManager dailyTaskManager;
    internal override void Start()
    {
        base.Start();
        dailyTaskManager = FindObjectOfType<DailyTaskManager>();
        CloseButtons();
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;


        PlayerPrefs.SetInt(Key.Button1_Level + transform.name, PlayerPrefs.GetInt(Key.Button1_Level + transform.name) + 1);
        PlayerPrefs.SetFloat(Key.Button1_Money + transform.name, (PlayerPrefs.GetFloat(Key.Button1_Money + transform.name) + upgrade1IncreasingMoneyAmountPerLevel));

        upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.Button1_Money + transform.name).ToString(MoneyManager.Instance.moneyFormat);
        upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button1_Level + transform.name).ToString();

        SetUpgradeSystem();
        upgrade1GameObject.SetActive(false);
        cleanerAI.SetActive(true);

        GameAnalytics.NewDesignEvent("Rewarded: " + "HireEmploye" + "ShoesArea" + ":" + Key.GetRewardedPlacementId());

        if (dailyTaskManager != null)
            dailyTaskManager.SetValue(Missions.hireEmploye, 1);
    }
    private void Events_onRewardedVideoAdClosedEvent(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;
    }

    private void Events_onRewardedVideoAdRewardedEventSecond(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;

        PlayerPrefs.SetInt(Key.Button2_Level + transform.name, PlayerPrefs.GetInt(Key.Button2_Level + transform.name) + 1);
        PlayerPrefs.SetFloat(Key.Button2_Money + transform.name, (PlayerPrefs.GetFloat(Key.Button2_Money + transform.name) + upgrade2IncreasingMoneyAmountPerLevel));

        upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.Button2_Money + transform.name).ToString(MoneyManager.Instance.moneyFormat);
        upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button2_Level + transform.name).ToString();

        SetUpgradeSystem();
        upgrade2GameObject.SetActive(false);
        receptionAI.SetActive(true);


        GameAnalytics.NewDesignEvent("Rewarded: " + "HireEmploye" + "Reception" + ":" + Key.GetRewardedPlacementId());

        if (dailyTaskManager != null)
            dailyTaskManager.SetValue(Missions.hireEmploye, 1);
    }
    private void Events_onRewardedVideoAdClosedEventSecond(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;
    }

    private void Events_onRewardedVideoAdRewardedEventThird(VideoAdReward arg1, AdInfo arg2)
    {
        upgrade3GameObject.SetActive(false);
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventThird;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventThird;

        PlayerPrefs.SetInt(Key.Button3_Level + transform.name, PlayerPrefs.GetInt(Key.Button3_Level + transform.name) + 1);
        PlayerPrefs.SetFloat(Key.Button3_Money + transform.name, (PlayerPrefs.GetFloat(Key.Button3_Money + transform.name) + upgrade3IncreasingMoneyAmountPerLevel));

        upgrade3MoneyText.text = PlayerPrefs.GetFloat(Key.Button3_Money + transform.name).ToString(MoneyManager.Instance.moneyFormat);
        upgarede3LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button3_Level + transform.name).ToString();

        SetUpgradeSystem();
        upgrade3GameObject.SetActive(false);
        ballCollectorAI.SetActive(true);


        GameAnalytics.NewDesignEvent("Rewarded: " + "HireEmploye" + "BallStacker" + ":" + Key.GetRewardedPlacementId());

        if (dailyTaskManager != null)
            dailyTaskManager.SetValue(Missions.hireEmploye, 1);


    }
    private void Events_onRewardedVideoAdClosedEventThird(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventThird;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventThird;
    }

    void OnClickUpgrade1()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade1MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.Button1_Level + transform.name, PlayerPrefs.GetInt(Key.Button1_Level + transform.name) + 1);
            SetMoney(PlayerPrefs.GetFloat(Key.Button1_Money + transform.name));
            PlayerPrefs.SetFloat(Key.Button1_Money + transform.name, (PlayerPrefs.GetFloat(Key.Button1_Money + transform.name) + upgrade1IncreasingMoneyAmountPerLevel));

            upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.Button1_Money + transform.name).ToString(MoneyManager.Instance.moneyFormat);
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button1_Level + transform.name).ToString();

            SetUpgradeSystem();
            upgrade1GameObject.SetActive(false);
            cleanerAI.SetActive(true);
            if (dailyTaskManager != null)
                dailyTaskManager.SetValue(Missions.hireEmploye, 1);
        }
    }

    void OnClickUpgrade1Ads()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    void OnClickUpgrade2()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade2MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.Button2_Level + transform.name, PlayerPrefs.GetInt(Key.Button2_Level + transform.name) + 1);
            SetMoney(PlayerPrefs.GetFloat(Key.Button2_Money + transform.name));
            PlayerPrefs.SetFloat(Key.Button2_Money + transform.name, (PlayerPrefs.GetFloat(Key.Button2_Money + transform.name) + upgrade2IncreasingMoneyAmountPerLevel));

            upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.Button2_Money + transform.name).ToString(MoneyManager.Instance.moneyFormat);
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button2_Level + transform.name).ToString();

            SetUpgradeSystem();
            upgrade2GameObject.SetActive(false);
            receptionAI.gameObject.SetActive(true);
            if (dailyTaskManager != null)
                dailyTaskManager.SetValue(Missions.hireEmploye, 1);
        }
    }
    void OnClickUpgrade2Ads()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEventSecond;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }
    void OnClickUpgrade3()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade3MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.Button3_Level + transform.name, PlayerPrefs.GetInt(Key.Button3_Level + transform.name) + 1);
            SetMoney(PlayerPrefs.GetFloat(Key.Button3_Money + transform.name));
            PlayerPrefs.SetFloat(Key.Button3_Money + transform.name, (PlayerPrefs.GetFloat(Key.Button3_Money + transform.name) + upgrade3IncreasingMoneyAmountPerLevel));

            upgrade3MoneyText.text = PlayerPrefs.GetFloat(Key.Button3_Money + transform.name).ToString(MoneyManager.Instance.moneyFormat);
            upgarede3LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button3_Level + transform.name).ToString();

            SetUpgradeSystem();
            upgrade3GameObject.SetActive(false);
            ballCollectorAI.SetActive(true);
            if (dailyTaskManager != null)
                dailyTaskManager.SetValue(Missions.hireEmploye, 1);
        }
    }
    void OnClickUpgrade3Ads()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEventThird;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEventThird;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }


    internal override void InitObjects()
    {
        base.InitObjects();
        upgrade1Button.onClick.AddListener(OnClickUpgrade1);
        upgrade2Button.onClick.AddListener(OnClickUpgrade2);
        upgrade3Button.onClick.AddListener(OnClickUpgrade3);

        upgrade1ButtonAds.onClick.AddListener(OnClickUpgrade1Ads);
        upgrade2ButtonAds.onClick.AddListener(OnClickUpgrade2Ads);
        upgrade3ButtonAds.onClick.AddListener(OnClickUpgrade3Ads);
    }

    internal void CloseButtons()
    {
        transform.Find("Background").gameObject.SetActive(false);
        transform.Find("WorkerBg").gameObject.SetActive(false);
        transform.Find("Title").gameObject.SetActive(false);
        transform.parent.GetComponent<Canvas>().sortingOrder = 0;
        if (upgrade1GameObject != null)
            upgrade1GameObject.SetActive(false);
        if (upgrade2GameObject != null)
            upgrade2GameObject.SetActive(false);
        if (upgrade3GameObject != null)
            upgrade3GameObject.SetActive(false);
    }
    internal void OpenButtons()
    {
        transform.Find("Background").gameObject.SetActive(true);
        transform.Find("WorkerBg").gameObject.SetActive(true);
        transform.Find("Title").gameObject.SetActive(true);
        SetUpgradeSystem();
        transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        if (upgrade1GameObject != null && PlayerPrefs.GetInt(Key.Button1_Level + transform.name) == 0)
            upgrade1GameObject.SetActive(true);
        if (upgrade2GameObject != null && PlayerPrefs.GetInt(Key.Button2_Level + transform.name) == 0)
            upgrade2GameObject.SetActive(true);
        if (upgrade3GameObject != null && PlayerPrefs.GetInt(Key.Button3_Level + transform.name) == 0)
            upgrade3GameObject.SetActive(true);
    }
}