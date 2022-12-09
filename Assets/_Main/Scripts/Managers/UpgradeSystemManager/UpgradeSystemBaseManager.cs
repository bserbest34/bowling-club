using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeSystemBaseManager : MonoBehaviour
{
    [Header("Number of Buttons You Want to Use :")]
    public int upgradeButtonCount = 2;
    [Space(30)]

    public int upgrade1BeginMoney;
    public int upgrade1IncreasingMoneyAmountPerLevel;

    public int upgrade2BeginMoney;
    public int upgrade2IncreasingMoneyAmountPerLevel;


    internal GameObject upgrade1GameObject;
    internal TextMeshProUGUI upgarede1LevelText;
    internal TextMeshProUGUI upgrade1MoneyText;
    internal Button upgrade1Button;
    internal Button upgrade1ButtonAds;

    internal GameObject upgrade2GameObject;
    internal TextMeshProUGUI upgarede2LevelText;
    internal TextMeshProUGUI upgrade2MoneyText;
    internal Button upgrade2Button;
    internal Button upgrade2ButtonAds;

    internal virtual void Start()
    {
        InitObjects();
        SetUpgradeSystem();
        SetActiveUpgradeButtons();
    }

    internal void SetUpgradeSystem()
    {
        SetUpgrade1UpgradeSystem();
        SetUpgrade2UpgradeSystem();
    }

    void SetUpgrade1UpgradeSystem()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= PlayerPrefs.GetFloat(Key.Button1_Money))
        {
            upgrade1Button.interactable = true;
        }
        else
        {
            upgrade1Button.interactable = false;
        }
    }

    void SetUpgrade2UpgradeSystem()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= PlayerPrefs.GetFloat(Key.Button2_Money))
        {
            upgrade2Button.interactable = true;
        }
        else
        {
            upgrade2Button.interactable = false;
        }
    }

    internal void SetMoney(float number)
    {
        MoneyManager.Instance.IncreaseMoneyAndWrite(-number);
    }

    internal virtual void InitObjects()
    {
        // Upgrade 1
        upgrade1GameObject = transform.Find("Upgrade1").gameObject;
        upgrade1Button = upgrade1GameObject.transform.Find("NoAds").GetComponent<Button>();
        upgrade1ButtonAds = upgrade1GameObject.transform.Find("Ads").GetComponent<Button>();
        upgarede1LevelText = transform.Find("Upgrade1").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade1MoneyText = transform.Find("Upgrade1").transform.Find("NoAds").Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        // Upgrade 2
        upgrade2GameObject = transform.Find("Upgrade2").gameObject;
        upgrade2Button = upgrade2GameObject.transform.Find("NoAds").GetComponent<Button>();
        upgrade2ButtonAds = upgrade2GameObject.transform.Find("Ads").GetComponent<Button>();
        upgarede2LevelText = transform.Find("Upgrade2").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade2MoneyText = transform.Find("Upgrade2").transform.Find("NoAds").Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        ConfigureInitializedObjects();
    }

    void ConfigureInitializedObjects()
    {
        //Set Level Text
        if (PlayerPrefs.GetInt(Key.Button1_Level) == 0)
        {
            PlayerPrefs.SetInt(Key.Button1_Level, 1);
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button1_Level).ToString();
        }
        else
        {
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button1_Level).ToString();
        }

        if (PlayerPrefs.GetInt(Key.Button2_Level) == 0)
        {
            PlayerPrefs.SetInt(Key.Button2_Level, 1);
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button2_Level).ToString();
        }
        else
        {
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.Button2_Level).ToString();
        }

        //Set Money Text
        if (PlayerPrefs.GetFloat(Key.Button1_Money) == 0)
        {
            PlayerPrefs.SetFloat(Key.Button1_Money, upgrade1BeginMoney);
            upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.Button1_Money).ToString(MoneyManager.Instance.moneyFormat);
        }
        else
        {
            upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.Button1_Money).ToString(MoneyManager.Instance.moneyFormat);
        }

        if (PlayerPrefs.GetFloat(Key.Button2_Money) == 0)
        {
            PlayerPrefs.SetFloat(Key.Button2_Money, upgrade2BeginMoney);
            upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.Button2_Money).ToString(MoneyManager.Instance.moneyFormat);
        }
        else
        {
            upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.Button2_Money).ToString(MoneyManager.Instance.moneyFormat);
        }
    }

    void SetActiveUpgradeButtons()
    {
        switch (upgradeButtonCount)
        {
            case 1:
                upgrade1GameObject.SetActive(true);
                break;
            case 2:
                upgrade1GameObject.SetActive(true);
                upgrade2GameObject.SetActive(true);
                break;
        }
    }
}