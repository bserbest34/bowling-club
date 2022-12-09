using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DaysType
{
    currentDay, beforeDay, nextDay
}

public class DailyRewardManager : MonoBehaviour
{
    public Button claim;
    int lastCollectedDay = 0;
    public List<GameObject> days = new List<GameObject>();
    public List<int> rewardMoney = new List<int>();

    private void Awake()
    {
        if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone", 0) == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (lastCollectedDay == 7)
        {
            lastCollectedDay = 0;
        }

        lastCollectedDay = PlayerPrefs.GetInt("lastCollectedDay", 0);

        if(DateTime.Now.DayOfYear - PlayerPrefs.GetInt("lastDayOfTime") == 0)
        {
            gameObject.SetActive(false);
        }
        else if(DateTime.Now.DayOfYear - PlayerPrefs.GetInt("lastDayOfTime") == 1)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.SetAsLastSibling();
                transform.GetChild(i).gameObject.SetActive(true);
            }
            claim.interactable = true;
        }else
        {
            PlayerPrefs.SetInt("lastCollectedDay", 0);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.SetAsLastSibling();
            }
            claim.interactable = true;
        }
        SetDaysUI();
    }

    void SetDaysUI()
    {
        for (int i = 0; i < 7; i++)
        {
            if(i > lastCollectedDay)
            {
                SetDay(days[i], DaysType.nextDay);
                continue;
            }
            if (i == lastCollectedDay)
            {
                SetDay(days[i], DaysType.currentDay);
                continue;
            }
            if (i < lastCollectedDay)
            {
                SetDay(days[i], DaysType.beforeDay);
            }
        }
    }

    void SetDay(GameObject day, DaysType type)
    {
        switch (type)
        {
            case DaysType.currentDay:
                day.transform.Find("DailyLockedBg").gameObject.SetActive(false);
                day.transform.Find("DailyLockedGlow").gameObject.SetActive(true);
                day.transform.Find("DailyUnLockedBg").gameObject.SetActive(true);
                day.transform.Find("Day").GetComponent<TextMeshProUGUI>().color = new Color(61, 114, 0);
                break;
            case DaysType.beforeDay:
                day.transform.Find("DailyLockedBg").gameObject.SetActive(false);
                day.transform.Find("DailyLockedGlow").gameObject.SetActive(false);
                day.transform.Find("DailyUnLockedBg").gameObject.SetActive(true);
                day.transform.Find("Day").GetComponent<TextMeshProUGUI>().color = new Color(61, 114, 0);
                break;
            case DaysType.nextDay:
                day.transform.Find("DailyLockedBg").gameObject.SetActive(true);
                day.transform.Find("DailyLockedGlow").gameObject.SetActive(false);
                day.transform.Find("DailyUnLockedBg").gameObject.SetActive(false);
                day.transform.Find("Day").GetComponent<TextMeshProUGUI>().color = new Color(198, 94, 0);
                break;
        }
    }

    void Start()
    {
        claim.onClick.AddListener(Claim);

        for (int i = 0; i < days.Count; i++)
        {
            days[i].transform.Find("RewardName").GetComponent<TextMeshProUGUI>().text = rewardMoney[i] + "$";
        }
    }

    void Claim()
    {
        Vibrations.Selection();
        lastCollectedDay++;
        switch (lastCollectedDay)
        {
            case 1:
                MoneyManager.Instance.CreateMoney(rewardMoney[0] / 50, true, days[0].transform.position, true);
                break;
            case 2:
                MoneyManager.Instance.CreateMoney(rewardMoney[1] / 50, true, days[1].transform.position, true);
                break;
            case 3:
                MoneyManager.Instance.CreateMoney(rewardMoney[2] / 50, true, days[2].transform.position, true);
                break;
            case 4:
                MoneyManager.Instance.CreateMoney(rewardMoney[3] / 50, true, days[3].transform.position, true);
                break;
            case 5:
                MoneyManager.Instance.CreateMoney(rewardMoney[4] / 50, true, days[4].transform.position, true);
                break;
            case 6:
                MoneyManager.Instance.CreateMoney(rewardMoney[5] / 50, true, days[5].transform.position, true);
                break;
            case 7:
                MoneyManager.Instance.CreateMoney(rewardMoney[6] / 50, true, days[6].transform.position, true);
                break;
        }
        PlayerPrefs.SetInt("lastCollectedDay", lastCollectedDay);
        PlayerPrefs.SetInt("lastDayOfTime", DateTime.Now.DayOfYear);
        StartCoroutine(SetFalse());
        if (lastCollectedDay > 6)
            lastCollectedDay = 0;
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}