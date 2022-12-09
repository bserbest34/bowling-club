using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomaGames.HomaBelly;
using GameAnalyticsSDK;

[System.Serializable]
public class OnboardingPoint
{
    public Transform transform;
    public GameObject camera;
    public bool isShowed;
    public GameObject nextOpenGameObject;
    public BoxCollider nextOpenCollider;
}

public class OnboardingManager : MonoBehaviour
{
    public float transitionTimeBetweenSteps = 1.5f;
    public float transitionTimeBetweenCameras = 1.5f;
    public GameObject firstMonies;
    public List<GameObject> needCloseGameobjects = new List<GameObject>();
    public List<BoxCollider> needCloseColliders = new List<BoxCollider>();

    public List<OnboardingPoint> onboardingPoints = new List<OnboardingPoint>();

    Material arrow;
    Vector3 currentTarget = Vector3.zero;
    bool isInTransitioProcess = false;
    bool isAllTutorialDone = false;
    public DailyRewardManager dailyRewardManager;
    public DailyTaskManager dailyTaskManager;
    public MyCollectionManager myCollectionManager;
    public BallCollector ballCollector;
    float time = 0;

    private void Awake()
    {
        time = Time.time;

        isAllTutorialDone = PlayerPrefs.GetInt("OnboardingisAllTutorialDone", 0) != 0;
        SetIsShowedValues();
        foreach (var item in onboardingPoints)
        {
            if (item.isShowed)
            {
                item.transform.GetComponent<OnBoardingPointScript>().isCol = true;
                if (item.nextOpenGameObject != null)
                    item.nextOpenGameObject.SetActive(true);

                if (item.nextOpenCollider != null)
                    item.nextOpenCollider.enabled = true;
            }
            else
            {
                item.transform.GetComponent<OnBoardingPointScript>().isCol = false;
                if (item.nextOpenGameObject != null)
                    item.nextOpenGameObject.SetActive(false);

                if (item.nextOpenCollider != null)
                    item.nextOpenCollider.enabled = false;
            }
        }

        if (!isAllTutorialDone)
        {
            foreach (var item in needCloseGameobjects)
            {
                item.SetActive(false);
            }

            foreach (var item in needCloseColliders)
            {
                item.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void Start()
    {
        SetOnboardingStep();
        arrow = transform.Find("Cube").GetComponent<MeshRenderer>().materials[0];


        Events.onBannerAdLoadedEvent += BannerLoaded;
        Events.onInterstitialAdClosedEvent += InterstialClosed;

        Events.onRewardedVideoAdClosedEvent += Events_onRewardedVideoAdClosedEvent;
        Events.onRewardedVideoAdStartedEvent += Events_onRewardedVideoAdStartedEvent;
        Events.onInterstitialAdOpenedEvent += Events_onInterstitialAdOpenedEvent;


        if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone") == 1)
        {
            StartCoroutine(SetBanner());
        }
        else
        {
            DefaultAnalytics.TutorialStepStarted("Onboarding");
            if (MoneyManager.Instance.money <= 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    firstMonies.GetComponent<Moneys>().moneys[i].SetActive(true);
                    firstMonies.GetComponent<Moneys>().openMoneyCount = 30;
                }
            }
        }
    }

    private void Events_onInterstitialAdOpenedEvent(AdInfo obj)
    {
        HomaBelly.Instance.HideBanner(Key.GetBannerPlacementId());
    }

    private void Events_onRewardedVideoAdStartedEvent(AdInfo obj)
    {
        HomaBelly.Instance.HideBanner(Key.GetBannerPlacementId());
    }


    private void Events_onRewardedVideoAdClosedEvent(AdInfo obj)
    {
        HomaBelly.Instance.ShowBanner(Key.GetBannerPlacementId());
        time = Time.time;
    }

    private void BannerLoaded(AdInfo adInfo)
    {
        Debug.Log($"Successfully showed ad {adInfo.PlacementId}");
        HomaBelly.Instance.ShowBanner(Key.GetBannerPlacementId());
    }

    private void InterstialClosed(AdInfo adInfo)
    {
        HomaBelly.Instance.ShowBanner(Key.GetBannerPlacementId());
        time = Time.time;
    }

    private void Update()
    {
        if (Time.time - time >= 120 && PlayerPrefs.GetInt("OnboardingisAllTutorialDone") == 1)
        {
            GameAnalytics.NewDesignEvent("Interstitials:" + "Regular" + ":" + Key.GetIntsPlacementId());
            HomaBelly.Instance.ShowInterstitial(Key.GetIntsPlacementId());
            time = Time.time;
        }
    }

    private void LateUpdate()
    {
        SetAnimation();
    }

    void SetAnimation()
    {
        if(currentTarget != Vector3.zero)
        {
            transform.Find("Cube").gameObject.SetActive(true);
            transform.LookAt(new Vector3(currentTarget.x, transform.position.y, currentTarget.z));
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
                Vector3.Distance(new Vector3(currentTarget.x, transform.position.y, currentTarget.z), transform.position));
            arrow.mainTextureOffset = new Vector2(0, arrow.mainTextureOffset.y + 1f * Time.deltaTime);
            arrow.mainTextureScale = new Vector2(1, transform.localScale.z / 3);
        }else
        {
            transform.Find("Cube").gameObject.SetActive(false);
        }
    }

    void SetIsShowedValues()
    {
        foreach (var item in onboardingPoints)
        {
            if(PlayerPrefs.GetInt("IsShowed" + item.transform.name, 0) == 0)
            {
                if(item.transform.name == "DistrubBallPoint4")
                {
                    item.isShowed = false;
                    PlayerPrefs.SetInt("IsShowed" + "GetBallPoint3", 0);
                    SetIsShowedValues();
                    return;
                }

                if(item.transform.name == "AcceptCustomerPoint5")
                {
                    int index = onboardingPoints.FindIndex(x => x.transform == item.transform);
                    PlayerPrefs.SetInt("IsShowed" + "GetBallPoint3", 0);
                    PlayerPrefs.SetInt("IsShowed" + "DistrubBallPoint4", 0);
                    item.isShowed = false;
                    onboardingPoints[index - 1].isShowed = false;
                    onboardingPoints[index - 2].isShowed = false;
                    SetIsShowedValues();
                    return;
                }

                if (item.transform.name == "DistrubBallPoint9")
                {
                    item.isShowed = false;
                    PlayerPrefs.SetInt("IsShowed" + "GetBallPoint8", 0);
                    SetIsShowedValues();
                    return;
                }

                if (item.transform.name == "AcceptCustomerPoint10")
                {
                    int index = onboardingPoints.FindIndex(x => x.transform == item.transform);

                    onboardingPoints[index - 1].isShowed = false;
                    onboardingPoints[index - 2].isShowed = false;

                    item.isShowed = false;
                    PlayerPrefs.SetInt("IsShowed" + "GetBallPoint8", 0);
                    PlayerPrefs.SetInt("IsShowed" + "DistrubBallPoint9", 0);
                    SetIsShowedValues();
                    return;
                }


                if (item.transform.name == "AcceptShoesCustomerPoint11")
                {
                    int index = onboardingPoints.FindIndex(x => x.transform == item.transform);

                    onboardingPoints[index - 1].isShowed = false;
                    onboardingPoints[index - 2].isShowed = false;
                    onboardingPoints[index - 3].isShowed = false;

                    item.isShowed = false;
                    PlayerPrefs.SetInt("IsShowed" + "GetBallPoint8", 0);
                    PlayerPrefs.SetInt("IsShowed" + "DistrubBallPoint9", 0);
                    PlayerPrefs.SetInt("IsShowed" + "AcceptCustomerPoint10", 0);
                    
                    SetIsShowedValues();
                    return;
                }
                return;
            }
            else
            {
                item.isShowed = true;
            }
        }
    }

    IEnumerator SetBanner()
    {
        yield return new WaitForSeconds(5f);
        HomaBelly.Instance.LoadBanner(Key.GetBannerPlacementId());
    }

    internal void SetShowed(Transform tra)
    {

        if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone") == 1)
        {
            currentTarget = Vector3.zero;
            return;
        }
        if (isInTransitioProcess)
            return;
        if (onboardingPoints.Find(x => x.transform == tra).isShowed == true)
            return;
        isInTransitioProcess = true;
        if (onboardingPoints.FindIndex(x => x.transform == tra) == onboardingPoints.Count - 1)
        {
            DefaultAnalytics.TutorialStepCompleted();
            PlayerPrefs.SetInt("OnboardingisAllTutorialDone", 1);
            ballCollector.dailyTaskManager = dailyTaskManager;
            StartCoroutine(SetBanner());

            GameAnalytics.NewDesignEvent("Interstitials:" + "OnboardingCompleted" + ":" + Key.GetIntsPlacementId());
            StartCoroutine(SetIntVideoFirst());

            if (dailyTaskManager != null)
                dailyTaskManager.gameObject.SetActive(true);


            if (myCollectionManager != null)
                myCollectionManager.gameObject.SetActive(true);


            if (dailyRewardManager != null)
                dailyRewardManager.gameObject.SetActive(true);

            if (MoneyManager.Instance != null)
                MoneyManager.Instance.dailyTaskManager = dailyTaskManager;
            foreach (var item in FindObjectsOfType<BuyNewThing>())
            {
                item.dailyTaskManager = dailyTaskManager;
            }
            foreach (var item in needCloseGameobjects)
            {
                item.SetActive(true);
            }
            foreach (var item in needCloseColliders)
            {
                item.GetComponent<BoxCollider>().enabled = true;
            }
        }

        if(onboardingPoints.Find(x => x.transform == tra) != null)
        {
            if(onboardingPoints.Find(x => x.transform == tra).nextOpenGameObject != null)
                onboardingPoints.Find(x => x.transform == tra).nextOpenGameObject.SetActive(true);
            if (onboardingPoints.Find(x => x.transform == tra).nextOpenCollider != null)
                onboardingPoints.Find(x => x.transform == tra).nextOpenCollider.enabled = true;

            onboardingPoints.Find(x => x.transform == tra).isShowed = true;
            PlayerPrefs.SetInt("IsShowed" + onboardingPoints.Find(x => x.transform == tra).transform.name, 1);
        }
        currentTarget = Vector3.zero;
        SetOnboardingStep();
    }

    IEnumerator SetIntVideoFirst()
    {
        yield return new WaitForSeconds(3f);
        HomaBelly.Instance.ShowInterstitial(Key.GetIntsPlacementId());
    }

    void SetOnboardingStep()
    {
        foreach (var item in onboardingPoints)
        {
            if(!item.isShowed)
            {
                StartCoroutine(TransitionToNextStep(item));
                break;
            }
        }
    }

    IEnumerator TransitionToNextStep(OnboardingPoint item)
    {
        yield return new WaitForSeconds(transitionTimeBetweenSteps);
        currentTarget = item.transform.position;
        if(item.camera != null)
        {
            item.camera.SetActive(true);
            StartCoroutine(SetFalseCamera(item.camera));
        }

        isInTransitioProcess = false;
    }

    IEnumerator SetFalseCamera(GameObject cam)
    {
        yield return new WaitForSeconds(transitionTimeBetweenCameras);
        cam.SetActive(false);
    }
}