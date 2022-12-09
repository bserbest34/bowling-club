using System.Collections;
using UnityEngine;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class UpgradeSystemManager : UpgradeSystemBaseManager
{
    BallCollector ballCollector;
    JoystickControl joystickControl;

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;

        PlayerPrefs.SetInt(Key.Button1_Level, PlayerPrefs.GetInt(Key.Button1_Level) + 1);
        PlayerPrefs.SetFloat(Key.Button1_Money, (PlayerPrefs.GetFloat(Key.Button1_Money) + upgrade1IncreasingMoneyAmountPerLevel));

        upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.Button1_Money).ToString(MoneyManager.Instance.moneyFormat);
        upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button1_Level).ToString();

        SetUpgradeSystem();
        ballCollector.StackUpgrade();

        GameAnalytics.NewDesignEvent("Rewarded: " + "UpgradeChar" + "Stack" + ":" + Key.GetRewardedPlacementId());

        StartCoroutine(SetVFX());
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

        PlayerPrefs.SetInt(Key.Button2_Level, PlayerPrefs.GetInt(Key.Button2_Level) + 1);
        PlayerPrefs.SetFloat(Key.Button2_Money, (PlayerPrefs.GetFloat(Key.Button2_Money) + upgrade2IncreasingMoneyAmountPerLevel));

        upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.Button2_Money).ToString(MoneyManager.Instance.moneyFormat);
        upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button2_Level).ToString();

        SetUpgradeSystem();
        joystickControl.SpeedUpgrade();

        GameAnalytics.NewDesignEvent("Rewarded: " + "UpgradeChar" + "Speed" + ":" + Key.GetRewardedPlacementId());

        StartCoroutine(SetVFX());
    }
    private void Events_onRewardedVideoAdClosedEventSecond(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;
    }

    internal override void Start()
    {
        ballCollector = FindObjectOfType<BallCollector>();
        joystickControl = FindObjectOfType<JoystickControl>();
        base.Start();
        CloseButtons();
    }

    void OnClickUpgrade1()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade1MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.Button1_Level, PlayerPrefs.GetInt(Key.Button1_Level) + 1);
            SetMoney(PlayerPrefs.GetFloat(Key.Button1_Money));
            PlayerPrefs.SetFloat(Key.Button1_Money, (PlayerPrefs.GetFloat(Key.Button1_Money) + upgrade1IncreasingMoneyAmountPerLevel));

            upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.Button1_Money).ToString(MoneyManager.Instance.moneyFormat);
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button1_Level).ToString();

            SetUpgradeSystem();
            ballCollector.StackUpgrade();
            StartCoroutine(SetVFX());
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
            PlayerPrefs.SetInt(Key.Button2_Level, PlayerPrefs.GetInt(Key.Button2_Level) + 1);
            SetMoney(PlayerPrefs.GetFloat(Key.Button2_Money));
            PlayerPrefs.SetFloat(Key.Button2_Money, (PlayerPrefs.GetFloat(Key.Button2_Money) + upgrade2IncreasingMoneyAmountPerLevel));

            upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.Button2_Money).ToString(MoneyManager.Instance.moneyFormat);
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button2_Level).ToString();

            SetUpgradeSystem();
            joystickControl.SpeedUpgrade();
            StartCoroutine(SetVFX());
        }
    }
    void OnClickUpgrade2Ads()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEventSecond;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    internal override void InitObjects()
    {
        base.InitObjects();
        upgrade1Button.onClick.AddListener(OnClickUpgrade1);
        upgrade2Button.onClick.AddListener(OnClickUpgrade2);
        upgrade1ButtonAds.onClick.AddListener(OnClickUpgrade1Ads);
        upgrade2ButtonAds.onClick.AddListener(OnClickUpgrade2Ads);
    }

    internal void CloseButtons()
    {
        transform.Find("Background").gameObject.SetActive(false);
        transform.Find("UpgradeBgImage").gameObject.SetActive(false);
        transform.Find("UpgradeTitle").gameObject.SetActive(false);
        transform.parent.GetComponent<Canvas>().sortingOrder = 0;
        if (upgrade1GameObject != null)
            upgrade1GameObject.SetActive(false);
        if (upgrade2GameObject != null)
            upgrade2GameObject.SetActive(false);
    }
    internal void OpenButtons()
    {
        transform.Find("Background").gameObject.SetActive(true);
        transform.Find("UpgradeBgImage").gameObject.SetActive(true);
        transform.Find("UpgradeTitle").gameObject.SetActive(true);
        SetUpgradeSystem();
        transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        if (upgrade1GameObject != null)
            upgrade1GameObject.SetActive(true);
        if (upgrade2GameObject != null)
            upgrade2GameObject.SetActive(true);
    }

    IEnumerator SetVFX()
    {
        ballCollector.transform.Find("Character").Find("LevelUp").gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        ballCollector.transform.Find("Character").Find("LevelUp").gameObject.SetActive(false);
    }
}