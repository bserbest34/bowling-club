using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : Singleton<MoneyManager>
{
    [SerializeField] private GameObject moneyObject;
    private List<GameObject> moneyList;
    private int moneyPoolCount = 60;

    [SerializeField] private Transform moneyTargetInUI;
    [SerializeField] private TextMeshProUGUI moneyText;

    [HideInInspector] public float money;

    internal float moneyObjectValue = 50;

    private Camera cam;
    internal string moneyFormat = "F0"; //F0 F1 F2
    public DailyTaskManager dailyTaskManager;
    internal int isPetEnabled = 1;

    private void Start()
    {
        InstantiateMoneyPool();
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Key.Money))
        {
            money = PlayerPrefs.GetFloat(Key.Money);
            moneyText.text = money.ToString(moneyFormat);
        }
        else
        {
            PlayerPrefs.SetFloat(Key.Money, 0);
            money = 0;
        }

        cam = Camera.main;
        UIManager.OnSuccess += SaveMoney;
    }

    private void InstantiateMoneyPool()
    {
        moneyList = new List<GameObject>();

        for (int i = 0; i < moneyPoolCount; i++)
        {
            GameObject money = Instantiate(moneyObject);
            money.transform.SetParent(transform);
            money.SetActive(false);

            moneyList.Add(money);
        }
    }

    public void CreateMoney(int count, bool saveMoneyImmediately, Vector3 position, bool isUI = false) // Use this
    {
        if(isUI)
        {
            for (int i = 0; i < count * isPetEnabled; i++)
            {
                StartCoroutine(CreateMoneyInUI(0.1f,
                    new Vector3(position.x + Random.Range(-50, 50), position.y + Random.Range(-50, 50)),
                    saveMoneyImmediately));
            }
        }
        else
        {
            position = cam.WorldToScreenPoint(position);
            for (int i = 0; i < count * isPetEnabled; i++)
            {
                StartCoroutine(CreateMoneyInUI(0.25f,
                    new Vector3(position.x + Random.Range(-150, 150), position.y + Random.Range(-150, 150), position.z + Random.Range(-150, 150)),
                    saveMoneyImmediately));
            }
        }

    }

    public void CreateMoney(int count, bool saveMoneyImmediately) // Use this
    {
        Vector3 position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        for (int i = 0; i < count; i++)
        {
            StartCoroutine(CreateMoneyInUI(i * 0.1f, position, saveMoneyImmediately));
        }
    }

    private IEnumerator CreateMoneyInUI(float time, Vector3 position, bool saveMoney)
    {
        yield return new WaitForSeconds(time);
        if(moneyList.Count <= 0)
        {
            IncreaseMoneyAndWrite(moneyObjectValue);
        }else
        {
            GameObject money = moneyList[0];
            money.SetActive(true);
            money.transform.position = position;
            money.SetActive(true);
            moneyList.RemoveAt(0);

            money.transform.DOMove(moneyTargetInUI.position, 1f).SetEase(Ease.InSine).OnComplete(() =>
            {
                if (saveMoney)
                    SaveMoney();

                money.SetActive(false);
                moneyList.Add(money);
                IncreaseMoneyAndWrite(moneyObjectValue);
            });
        }
    }

    public void IncreaseMoneyAndWrite(float addingMoney)
    {
        money += addingMoney;
        moneyText.text = money.ToString(moneyFormat);
        if(dailyTaskManager != null && addingMoney > 0)
            dailyTaskManager.SetValue(Missions.earnMoney, addingMoney);
        SaveMoney();
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetFloat(Key.Money, money);
    }
}