using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class MoneyBag : MonoBehaviour
{
    Button moneyBag;
    Button close;
    public TextMeshProUGUI text;
    int moneyValue;

    void Start()
    {
        moneyValue = PlayerPrefs.GetInt("MoneyValue", 1000);
        text.text = moneyValue.ToString() + "$";
        moneyBag = transform.Find("Buy").Find("GetButton").GetComponent<Button>();
        close = transform.Find("Buy").Find("Close").GetComponent<Button>();

        moneyBag.onClick.AddListener(MoneyBagClicked);
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

        GameAnalytics.NewDesignEvent("Rewarded: " + "MoneyBag" + ":" + Key.GetRewardedPlacementId());

        transform.parent.GetComponent<BoxCollider>().enabled = false;
        transform.Find("Buy").gameObject.SetActive(false);
        StartCoroutine(SetTrueAgain());
    }

    void MoneyBagClicked()
    {
        Vibrations.Selection();
        text.text = moneyValue.ToString() + "$";

        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    void Close()
    {
        transform.parent.GetComponent<BoxCollider>().enabled = false;
        transform.Find("Buy").gameObject.SetActive(false);
        transform.root.Find("MoneyBag").gameObject.SetActive(false);
        StartCoroutine(SetTrueAgainClose());
    }

    IEnumerator SetTrueAgain()
    {
        transform.parent.Find("MoneyBag").GetComponent<Animator>().SetTrigger("trigger");
        yield return new WaitForSeconds(2.5f);
        MoneyManager.Instance.CreateMoney((int)(moneyValue / MoneyManager.Instance.moneyObjectValue), true, transform.root.position);
        transform.parent.Find("MoneyBag").gameObject.SetActive(false);
        moneyValue += 250;
        PlayerPrefs.SetInt("MoneyValue", moneyValue);
        yield return new WaitForSeconds(120f);
        text.text = moneyValue.ToString() + "$";
        gameObject.SetActive(false);
        transform.parent.GetComponent<BoxCollider>().enabled = true;
        transform.Find("Buy").gameObject.SetActive(true);
        transform.parent.Find("MoneyBag").gameObject.SetActive(true);
    }

    IEnumerator SetTrueAgainClose()
    {
        yield return new WaitForSeconds(30f);
        text.text = moneyValue.ToString() + "$";
        gameObject.SetActive(false);
        transform.parent.GetComponent<BoxCollider>().enabled = true;
        transform.Find("Buy").gameObject.SetActive(true);
        transform.parent.Find("MoneyBag").gameObject.SetActive(true);
    }
}