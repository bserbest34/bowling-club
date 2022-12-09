using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

public class VIPBowlingArea : MonoBehaviour
{
    public float cost = 20;
    internal float openPart = 0;
    Image image;
    int isBought = 0;
    float time = 0;
    public GameObject vipChar;
    public GameObject bodyguard1, bodyguard2;
    public Button button;

    public Transform vipBowlingPoint;
    public Transform bodyguard1Point;
    public Transform bodyguard2Point;

    public Transform exitPoint;
    float lastVipTime;
    public Transform receptionArea;
    JoystickControl jControl;
    public GameObject vipCanvas;
    TextMeshProUGUI timerText;
    float counterTime = 120;
    float lastTime = 0;
    public GameObject vipCam;
    bool isCollected = false;

    void Start()
    {
        timerText = vipCanvas.transform.Find("TimeBg").Find("Time").GetComponent<TextMeshProUGUI>();
        jControl = FindObjectOfType<JoystickControl>();
        lastVipTime = Time.time - 120;
        button.onClick.AddListener(VipAccepted);
        isBought = PlayerPrefs.GetInt(transform.name + "VIP" + "isBought", 0);
        image = transform.Find("Canvas").Find("Image").GetComponent<Image>();
        if(isBought == 1)
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.Find("Canvas").gameObject.SetActive(false);
            transform.Find("UnluckBowlingArea").gameObject.SetActive(false);
            transform.Find("BowlingArea").gameObject.SetActive(true);
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            transform.Find("Canvas").gameObject.SetActive(true);
            transform.Find("UnluckBowlingArea").gameObject.SetActive(true);
            transform.Find("BowlingArea").gameObject.SetActive(false);
        }

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


        GameAnalytics.NewDesignEvent("Rewarded: " + "VIP" + transform.root.name + ":" + Key.GetRewardedPlacementId());

        isBought = 1;
        PlayerPrefs.SetInt(transform.name + "VIP" + "isBought", isBought);
        transform.Find("Canvas").gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        transform.Find("Canvas").gameObject.SetActive(false);
        transform.Find("UnluckBowlingArea").gameObject.SetActive(false);
        transform.Find("BowlingArea").gameObject.SetActive(true);
    }

    private void Events_onRewardedVideoAdClosedEventVIP(AdInfo obj)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventVIP;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventVIP;
    }

    private void Events_onRewardedVideoAdRewardedEventVIP(VideoAdReward arg1, AdInfo arg2)
    {
        Events.onRewardedVideoAdRewardedEvent -= Events_onRewardedVideoAdRewardedEventVIP;
        Events.onRewardedVideoAdClosedEvent -= Events_onRewardedVideoAdClosedEventVIP;

        GameAnalytics.NewDesignEvent("Rewarded: " + "VIPCustomer" + ":" + Key.GetRewardedPlacementId());

        vipCanvas.SetActive(false);
        StartCoroutine(SetCamera());
    }

    private void Update()
    {
        if(isBought == 1 && Time.time - lastVipTime > 180)
        {
            vipCanvas.SetActive(true);
            vipChar.SetActive(true);
            bodyguard1.SetActive(true);
            bodyguard2.SetActive(true);
            vipChar.GetComponent<VIPNavMesh>().enabled = true;
            bodyguard1.GetComponent<VIPNavMesh>().enabled = true;
            bodyguard2.GetComponent<VIPNavMesh>().enabled = true;
            lastVipTime = Time.time;
        }

        if(vipCanvas.activeInHierarchy)
        {
            if (counterTime > 0 && Time.time - lastTime > 1f)
            {
                DisplayTime(counterTime);
                lastTime = Time.time;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        counterTime--;
        if(timeToDisplay <= 1)
        {
            vipCanvas.SetActive(false);
            StartCoroutine(TimeFull());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCollected)
            return;
        if (other.CompareTag("Player") && jControl.isRelase && Time.time - time > 0.05)
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

    void VipAccepted()
    {
        Vibrations.Selection();
        Events.onRewardedVideoAdRewardedEvent += Events_onRewardedVideoAdRewardedEventVIP;
        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEventVIP;
        HomaBelly.Instance.ShowRewardedVideoAd(Key.GetRewardedPlacementId());
    }

    IEnumerator SetCamera()
    {
        vipCam.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        vipChar.GetComponent<VIPNavMesh>().isPlayeable = true;
        vipChar.GetComponent<VIPNavMesh>().enabled = true;
        bodyguard1.GetComponent<VIPNavMesh>().enabled = true;
        bodyguard2.GetComponent<VIPNavMesh>().enabled = true;
        vipChar.GetComponent<NavMeshAgent>().SetDestination(vipBowlingPoint.transform.position);
        bodyguard1.GetComponent<NavMeshAgent>().SetDestination(bodyguard1Point.transform.position);
        bodyguard2.GetComponent<NavMeshAgent>().SetDestination(bodyguard2Point.transform.position);
        yield return new WaitForSeconds(1f);
        vipCam.SetActive(false);
        receptionArea.Find("Monies").GetComponent<Moneys>().SetMoney(20, vipChar);
    }

    IEnumerator TimeFull()
    {
        vipCanvas.gameObject.SetActive(false);
        vipChar.GetComponent<NavMeshAgent>().SetDestination(exitPoint.position);
        yield return new WaitForSeconds(2f);
        bodyguard1.GetComponent<NavMeshAgent>().SetDestination(exitPoint.position);
        yield return new WaitForSeconds(2f);
        bodyguard2.GetComponent<NavMeshAgent>().SetDestination(exitPoint.position);
        counterTime = 120;
    }
}