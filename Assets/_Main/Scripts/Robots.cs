using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomaGames.HomaBelly;
using UnityEngine.UI;
using GameAnalyticsSDK;
public class Robots : MonoBehaviour
{
    Button groundRobot;
    Button flyRobot;
    Button close;

    void Start()
    {
        groundRobot = transform.Find("BuyGroundPet").Find("GetButton").GetComponent<Button>();
        flyRobot = transform.Find("BuyFlyPet").Find("GetButton").GetComponent<Button>();
        close = transform.Find("Close").GetComponent<Button>();

        groundRobot.onClick.AddListener(GroundRobotClicked);
        flyRobot.onClick.AddListener(FlyRobotClicked);
        close.onClick.AddListener(Close);
    }

    private void Events_onRewardedVideoAdClosedEvent(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;

        Close();
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;

        GameAnalytics.NewDesignEvent("Rewarded: " + "RobotFly" + ":" + Key.GetRewardedPlacementId());

        transform.parent.Find("RobotFly").GetComponent<PetFollow>().enabled = true;
        transform.parent.Find("RobotFly").parent = null;
        Close();
    }
    private void Events_onRewardedVideoAdClosedEventSecond(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;

        Close();
    }

    private void Events_onRewardedVideoAdRewardedEventSecond(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventSecond;

        GameAnalytics.NewDesignEvent("Rewarded: " + "RobotGround" + ":" + Key.GetRewardedPlacementId());

        transform.parent.Find("RobotGround").GetComponent<PetFollow>().enabled = true;
        transform.parent.Find("RobotGround").parent = null;
        Close();
    }

    void GroundRobotClicked()
    {
        Vibrations.Selection();

        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }
    void FlyRobotClicked()
    {
        Vibrations.Selection();

        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEventSecond;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEventSecond;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    void Close()
    {
        FindObjectOfType<SpawnManager>().SpawnRobots();
        transform.parent.gameObject.SetActive(false);
    }
}
