using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;
using UnityEngine.AI;

public class BallCollectorAIManager : MonoBehaviour
{
    Button aiCall;
    Button close;

    void Start()
    {
        aiCall = transform.Find("ShelfUpgradeOverlayCanvas").transform.Find("Buy").Find("GetButton").GetComponent<Button>();
        close = transform.Find("ShelfUpgradeOverlayCanvas").transform.Find("Buy").Find("Close").GetComponent<Button>();

        aiCall.onClick.AddListener(MoneyBagClicked);
        close.onClick.AddListener(Close);
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

        GameAnalytics.NewDesignEvent("Rewarded: " + "BallDistrubutorAIs" + ":" + Key.GetRewardedPlacementId());

        transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
        GetComponent<NavMeshObstacle>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(SetTrueAgain());
    }

    void MoneyBagClicked()
    {
        Vibrations.Selection();

        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    void Close()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        transform.Find("ShelfUpgradeOverlayCanvas").gameObject.SetActive(false);
        transform.Find("MoneyBag").gameObject.SetActive(false);
        transform.Find("Platform").gameObject.SetActive(false);
        StartCoroutine(SetTrueAgainClose());
    }

    IEnumerator SetTrueAgain()
    {
        for (int i = 0; i < transform.Find("MoneyBag").childCount; i++)
        {
            transform.Find("MoneyBag").GetChild(i).GetComponent<NavMeshAgent>().enabled = true;
            transform.Find("MoneyBag").GetChild(i).GetComponent<AICharacter>().enabled = true;
        }

        yield return new WaitForSeconds(30f);

        for (int i = 0; i < transform.Find("MoneyBag").childCount; i++)
        {
            foreach (var item in transform.Find("MoneyBag").GetChild(i).GetComponent<AICharacter>().stackingBallList)
            {
                Destroy(item);
            }
            transform.Find("MoneyBag").GetChild(i).GetComponent<AICharacter>().stackingBallList.Clear();
            transform.Find("MoneyBag").GetChild(i).GetComponent<NavMeshAgent>().enabled = true;
        }
        FindObjectOfType<SpawnManager>().SpawnBallCollectorAIs();
        Destroy(gameObject);
    }

    IEnumerator SetTrueAgainClose()
    {
        yield return new WaitForSeconds(30f);
        for (int i = 0; i < transform.Find("MoneyBag").childCount; i++)
        {
            transform.Find("MoneyBag").GetChild(i).GetComponent<NavMeshAgent>().enabled = false;
            transform.Find("MoneyBag").GetChild(i).GetComponent<NavMeshAgent>().enabled = false;
        }
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<NavMeshObstacle>().enabled = true;
        transform.Find("MoneyBag").gameObject.SetActive(true);
        transform.Find("Platform").gameObject.SetActive(true);
    }
}