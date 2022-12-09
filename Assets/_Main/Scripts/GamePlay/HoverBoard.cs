using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class HoverBoard : MonoBehaviour
{
    Button hoverboard;
    Button close;
    JoystickControl jControl;

    void Start()
    {
        jControl = FindObjectOfType<JoystickControl>();
        hoverboard = transform.Find("Buy").Find("GetButton").GetComponent<Button>();
        close = transform.Find("Buy").Find("Close").GetComponent<Button>();
        close.onClick.AddListener(Close);
        hoverboard.onClick.AddListener(SetRewardVideo);
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

        GameAnalytics.NewDesignEvent("Rewarded: " + "Hoverboard" + ":" + Key.GetRewardedPlacementId());

        transform.parent.Find("Hovboard").GetComponent<Animator>().SetTrigger("trigger");
        jControl.Clicked(transform.root.gameObject);
    }

    void SetRewardVideo()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    void Close()
    {
        StartCoroutine(SetActiveClose());
    }

    IEnumerator SetActiveClose()
    {
        transform.Find("Buy").gameObject.SetActive(false);
        transform.root.GetComponent<BoxCollider>().enabled = false;
        transform.root.Find("Hovboard").gameObject.SetActive(false);
        yield return new WaitForSeconds(30f);
        transform.Find("Buy").gameObject.SetActive(true);
        transform.root.GetComponent<BoxCollider>().enabled = true;
        transform.root.Find("Hovboard").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
