using UnityEngine;
using DG.Tweening;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class BowlingAreaManager : BowlingAreaBaseManager
{
    internal override void Start()
    {
        base.Start();
        CloseButtons();
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;


        PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name) + 1);
        SetUpgradeSystem();
        transform.root.GetComponent<BowlingArea>().SetBowlingAreaLevel();

        GameAnalytics.NewDesignEvent("Rewarded: " + "BowlingUpgrade" + ":" + Key.GetRewardedPlacementId());
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


        PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name) + 1);
        SetUpgradeSystem();
        transform.root.GetComponent<BowlingArea>().SetBowlingAreaLevel();

        GameAnalytics.NewDesignEvent("Rewarded: " + "BowlingUpgrade" + ":" + Key.GetRewardedPlacementId());
    }
    private void Events_onRewardedVideoAdClosedEventSecond(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;
    }

    private void Events_onRewardedVideoAdRewardedEventThird(VideoAdReward arg1, AdInfo arg2)
    {
        PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name) + 1);
        SetUpgradeSystem();
        transform.root.GetComponent<BowlingArea>().SetBowlingAreaLevel();

        GameAnalytics.NewDesignEvent("Rewarded: " + "BowlingUpgrade" + ":" + Key.GetRewardedPlacementId());
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
            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name) + 1);
            SetMoney(upgrade1BeginMoney);

            SetUpgradeSystem();
            transform.root.GetComponent<BowlingArea>().SetBowlingAreaLevel();
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
            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name) + 1);
            SetMoney(upgrade2BeginMoney);
            SetUpgradeSystem();
            transform.root.GetComponent<BowlingArea>().SetBowlingAreaLevel();
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
            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name) + 1);
            SetMoney(upgrade3BeginMoney);

            SetUpgradeSystem();
            transform.root.GetComponent<BowlingArea>().SetBowlingAreaLevel();
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
        SetUpgradeSystem();
        transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        switch (PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + transform.root.name))
        {
            case 1:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(true);
                if (upgrade2GameObject != null)
                    upgrade2GameObject.SetActive(true);
                if (upgrade3GameObject != null)
                    upgrade3GameObject.SetActive(false);
                break;
            case 2:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(false);

                if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone") == 0)
                    return;
                if (upgrade2GameObject != null)
                {
                    upgrade2GameObject.SetActive(true);
                    upgrade2GameObject.transform.DOMove(point1.transform.position, 0.5f);
                }
                if (upgrade3GameObject != null)
                {
                    upgrade3GameObject.SetActive(true);
                    upgrade3GameObject.transform.DOMove(point2.transform.position, 0.5f);
                }
                break;
            case 3:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(false);
                if (upgrade2GameObject != null)
                    upgrade2GameObject.SetActive(false);
                if (upgrade3GameObject != null)
                {
                    upgrade3GameObject.SetActive(true);
                    upgrade3GameObject.transform.DOMove(point3.transform.position, 0.5f);
                }
                break;
            case 4:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(false);
                if (upgrade2GameObject != null)
                    upgrade2GameObject.SetActive(false);
                if (upgrade3GameObject != null)
                    upgrade3GameObject.SetActive(false);
                break;
        }
    }
}