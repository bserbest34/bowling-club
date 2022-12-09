using UnityEngine;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class VipMiniGameCanvasManager : VipMiniGameBaseManager
{
    int shelfUpgrade = 0;
    internal override void Start()
    {
        shelfUpgrade = PlayerPrefs.GetInt(Key.ButtonShelfUpgrade + transform.root.name, 0);
        base.Start();
        ShelfUpgradeMain();
        transform.parent.GetComponent<Canvas>().sortingOrder = 1;
    }

    private void Events_onRewardedVideoAdClosedEvent(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;


        GameAnalytics.NewDesignEvent("Rewarded: " + "VIP" + transform.root.name + ":" + Key.GetRewardedPlacementId());

        PlayerPrefs.SetInt(Key.VipLangert + transform.root.name, 1);
        transform.root.Find("UnlockVIP").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void OnClickShelfUpgradeLevel2()
    {
        Vibrations.Selection();
        if (PlayerPrefs.GetFloat(Key.Money) < int.Parse(upgrade1MoneyText.text)) return;
        PlayerPrefs.SetInt(Key.VipLangert + transform.root.name, 0);
        transform.root.Find("Unlock").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnClickShelfUpgradeLevel3()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }


    internal override void InitObjects()
    {
        base.InitObjects();
        upgrade1Button.onClick.AddListener(OnClickShelfUpgradeLevel2);
        upgrade2Button.onClick.AddListener(OnClickShelfUpgradeLevel3);
    }

    internal void OpenButtons()
    {
        SetUpgradeSystem();
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
            case 1:
                transform.root.Find("Unlock").gameObject.SetActive(true);
                if (transform.Find("Sticks") != null)
                    transform.Find("Sticks").gameObject.SetActive(true);
                gameObject.SetActive(false);
                break;
            case 2:
                transform.root.Find("UnlockVIP").gameObject.SetActive(true);
                if (transform.Find("Sticks") != null)
                    transform.Find("Sticks").gameObject.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }
}