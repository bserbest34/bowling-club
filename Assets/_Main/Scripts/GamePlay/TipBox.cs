using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class TipBox : MonoBehaviour
{
    float time;
    Image image;
    public TextMeshProUGUI text;
    bool isCollected = false;
    int currentMoney = 0;
    float lastTime = 0;
    public List<GameObject> monies = new List<GameObject>();
    int currentOpenMoneyCount = 0;
    int moneyValue;

    void Start()
    {
        moneyValue = PlayerPrefs.GetInt("MoneyValue", 1000);
        image = transform.Find("Canvas").Find("Image").GetComponent<Image>();
    }

    private void Events_onRewardedVideoAdClosedEvent(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;
        image.fillAmount = 0;
        isCollected = false;
    }

    private void Events_onRewardedVideoAdRewardedEvent(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEvent;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEvent;
        foreach (var item in monies)
        {
            item.SetActive(false);
        }

        GameAnalytics.NewDesignEvent("Rewarded: " + "TipBox" + ":" + Key.GetRewardedPlacementId());
        moneyValue += 100;
        PlayerPrefs.SetInt("MoneyValue", moneyValue);
        MoneyManager.Instance.CreateMoney((int)(currentMoney / MoneyManager.Instance.moneyObjectValue), true, transform.Find("TipBoxObj").position);
        currentMoney = 0;
        text.text = 0 + "$";
        transform.Find("Canvas").gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        text.gameObject.SetActive(false);
        StartCoroutine(SetActiveAgain());
    }

    private void Update()
    {
        if(currentMoney < moneyValue && Time.time - lastTime > 0.5f && text.gameObject.activeInHierarchy)
        {
            currentOpenMoneyCount = currentMoney / 10;
            if (currentOpenMoneyCount < monies.Count)
            {
                monies[currentMoney/10].SetActive(true);
            }
            currentMoney++;
            text.text = currentMoney.ToString() + "$";
            lastTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCollected)
            return;
        if (other.CompareTag("Player") && FindObjectOfType<JoystickControl>().isRelase && Time.time - time > 0.05)
        {
            image.fillAmount += 0.05f;
            time = Time.time;
            if (image.fillAmount >= 1)
            {
                isCollected = true;
                Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEvent;
                Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
                HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            image.fillAmount = 0;
        }
    }

    IEnumerator SetActiveAgain()
    {
        yield return new WaitForSeconds(20f);
        image.fillAmount = 0;
        text.text = 0 + "$";
        currentMoney = 0;
        currentOpenMoneyCount = 0;
        text.gameObject.SetActive(true);
        transform.Find("Canvas").gameObject.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
        isCollected = false;
    }
}