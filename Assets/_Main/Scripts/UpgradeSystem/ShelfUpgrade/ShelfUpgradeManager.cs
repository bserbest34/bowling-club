using UnityEngine;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class ShelfUpgradeManager : ShelfUpgradeBaseManager
{
    int shelfUpgrade = 0;
    internal override void Start()
    {
        shelfUpgrade = PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name, 0);
        base.Start();
        CloseButtons();
        ShelfUpgradeMain();
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;


        PlayerPrefs.SetInt(Key.ButtonShelfUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name) + 1);
        ShelfUpgrade();
        ShelfUpgradeMain();
        SetUpgradeSystem();
        upgrade1GameObject.SetActive(false);
        upgrade2GameObject.transform.position = middlePoint.transform.position;

        GameAnalytics.NewDesignEvent("Rewarded: " + "ShelfUpgrade" + ":" + Key.GetRewardedPlacementId());
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

        PlayerPrefs.SetInt(Key.ButtonShelfUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name) + 1);
        ShelfUpgrade();
        ShelfUpgradeMain();
        SetUpgradeSystem();

        GameAnalytics.NewDesignEvent("Rewarded: " + "ShelfUpgrade" + ":" + Key.GetRewardedPlacementId());
    }
    private void Events_onRewardedVideoAdClosedEventSecond(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;
    }

    void OnClickShelfUpgradeLevel2()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade1MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.ButtonShelfUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name) + 1);
            SetMoney(upgrade1BeginMoney);
            ShelfUpgrade();
            ShelfUpgradeMain();
            SetUpgradeSystem();
            upgrade1GameObject.SetActive(false);
            upgrade2GameObject.transform.position = middlePoint.transform.position;
        }
    }

    void OnClickShelfUpgradeLevel3()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade2MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.ButtonShelfUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name) + 1);
            SetMoney(upgrade2BeginMoney);
            ShelfUpgrade();
            ShelfUpgradeMain();
            SetUpgradeSystem();
        }
    }

    void OnClickShelfUpgradeLevel2Ads()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }
    void OnClickShelfUpgradeLevel3Ads()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEventSecond;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    internal override void InitObjects()
    {
        base.InitObjects();
        upgrade1Button.onClick.AddListener(OnClickShelfUpgradeLevel2);
        upgrade2Button.onClick.AddListener(OnClickShelfUpgradeLevel3);


        upgrade1ButtonAds.onClick.AddListener(OnClickShelfUpgradeLevel2Ads);
        upgrade2ButtonAds.onClick.AddListener(OnClickShelfUpgradeLevel3Ads);
    }

    internal void CloseButtons()
    {
        transform.parent.GetComponent<Canvas>().sortingOrder = 0;

        switch (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name))
        {
            case 0:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(false);
                if (upgrade2GameObject != null)
                    upgrade2GameObject.SetActive(false);
                break;
            case 1:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(false);
                if (upgrade2GameObject != null)
                    upgrade2GameObject.SetActive(false);
                break;
        }
    }
    internal void OpenButtons()
    {
        SetUpgradeSystem();
        transform.parent.GetComponent<Canvas>().sortingOrder = 1;

        switch (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name))
        {
            case 0:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(true);
                if (upgrade2GameObject != null)
                    upgrade2GameObject.SetActive(true);
                upgrade1GameObject.transform.position = middlePoint.transform.position;
                break;
            case 1:
                if (upgrade1GameObject != null)
                    upgrade1GameObject.SetActive(false);
                if (upgrade2GameObject != null)
                {
                    upgrade2GameObject.SetActive(true);
                    upgrade2GameObject.transform.position = middlePoint.transform.position;
                }
                break;
        }
    }

    void ShelfUpgrade()
    {
        PlayerPrefs.SetInt(Key.ButtonShelfUpgrade + transform.root.name, shelfUpgrade + 1);
        shelfUpgrade = PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name);
    }
    void ShelfUpgradeMain()
    {
        switch (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name))
        {
            case 0:
                transform.root.Find("Shelfs").Find("ShelfLvl1").gameObject.SetActive(true);
                transform.root.Find("Shelfs").Find("BallsLevel1New").gameObject.SetActive(true);
                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().rafBalls.Clear();

                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().level = PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name);
                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().SetFirstBalls();
                break;
            case 1:
                transform.root.Find("Shelfs").Find("ShelfLvl1").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("BallsLevel1New").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("ShelfsLvl2").gameObject.SetActive(true);
                transform.root.Find("Shelfs").Find("BallsLevel2New").gameObject.SetActive(true);
                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().rafBalls.Clear();

                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().level = PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name);
                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().SetFirstBalls();
                break;
            case 2:
                transform.root.Find("Shelfs").Find("ShelfLvl1").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("BallsLevel1New").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("ShelfsLvl2").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("BallsLevel2New").gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("ShelfLvl3").gameObject.SetActive(true);
                transform.root.Find("Shelfs").Find("BallsLevel3New").gameObject.SetActive(true);
                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().rafBalls.Clear();
                transform.gameObject.SetActive(false);
                transform.root.Find("Shelfs").Find("UpgradeProductionTime").gameObject.SetActive(false);

                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().level = PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name);
                transform.root.Find("Shelfs").GetComponent<BallDistributorManager>().SetFirstBalls();
                break;
        }
    }
}