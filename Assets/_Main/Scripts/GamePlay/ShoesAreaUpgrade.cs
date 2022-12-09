using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class ShoesAreaUpgrade : ShoesAreaBaseUpgrade
{
    int shelfUpgrade = 0;
    internal override void Start()
    {
        shelfUpgrade = PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name, 0);
        base.Start();
        CloseButtons();
        ShelfUpgradeMain();
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;


        PlayerPrefs.SetInt(Key.ShoesUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name) + 1);
        ShelfUpgrade();
        SetUpgradeSystem();
        upgrade1GameObject.SetActive(false);
        upgrade2GameObject.transform.position = middlePoint.transform.position;
        ShelfUpgradeMain();

        GameAnalytics.NewDesignEvent("Rewarded: " + "ShoesAreaUpgrade" + ":" + Key.GetRewardedPlacementId());
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

        PlayerPrefs.SetInt(Key.ShoesUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name) + 1);
        ShelfUpgrade();
        SetUpgradeSystem();

        GameAnalytics.NewDesignEvent("Rewarded: " + "ShoesAreaUpgrade" + ":" + Key.GetRewardedPlacementId());
        ShelfUpgradeMain();
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
            PlayerPrefs.SetInt(Key.ShoesUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name) + 1);
            SetMoney(upgrade1BeginMoney);
            ShelfUpgrade();
            SetUpgradeSystem();
            upgrade1GameObject.SetActive(false);
            upgrade2GameObject.transform.position = middlePoint.transform.position;
            ShelfUpgradeMain();
        }
    }

    void OnClickShelfUpgradeLevel3()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) >= int.Parse(upgrade2MoneyText.text))
        {
            PlayerPrefs.SetInt(Key.ShoesUpgrade + transform.root.name, PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name) + 1);
            SetMoney(upgrade2BeginMoney);
            ShelfUpgrade();
            SetUpgradeSystem();
            ShelfUpgradeMain();
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

        switch (PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name))
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

        switch (PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name))
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
        PlayerPrefs.SetInt(Key.ShoesUpgrade + transform.root.name, shelfUpgrade + 1);
        shelfUpgrade = PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name);
    }
    void ShelfUpgradeMain()
    {
        switch (PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name))
        {
            case 0:
                transform.root.Find("Unlock").Find("Lvl1ShoesShelf").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("Lvl1Shoes").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("Lvl1ShoesNew").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().rafShoes.Clear();

                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().level = PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name);
                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().SetFirstShoes();
                break;
            case 1:
                transform.root.Find("Unlock").Find("Lvl1ShoesShelf").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl1Shoes").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl1ShoesNew").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl2ShoesShelf").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("Lvl2Shoes").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("Lvl2ShoesNew").gameObject.SetActive(true);

                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().rafShoes.Clear();

                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().level = PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name);
                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().SetFirstShoes();
                break;
            case 2:
                transform.root.Find("Unlock").Find("Lvl1ShoesShelf").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl1Shoes").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl1ShoesNew").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl2ShoesShelf").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl2Shoes").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl2ShoesNew").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl3ShoesShelf").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("Lvl3Shoes").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("Lvl3ShoesNew").gameObject.SetActive(true);
                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().rafShoes.Clear();
                transform.gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("ShoesUpgradeArea").gameObject.SetActive(false);

                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().level = PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name);
                transform.root.Find("Unlock").Find("CollectShoes").GetComponent<ShoesDistrubutor>().SetFirstShoes();
                break;
        }
    }
}
