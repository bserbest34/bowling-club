using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BowlingAreaBaseManager : MonoBehaviour
{
    [Header("Number of Buttons You Want to Use :")]
    public int upgradeButtonCount = 2;
    [Space(30)]

    public int upgrade1BeginMoney;
    public int upgrade1IncreasingMoneyAmountPerLevel;

    public int upgrade2BeginMoney;
    public int upgrade2IncreasingMoneyAmountPerLevel;

    public int upgrade3BeginMoney;
    public int upgrade3IncreasingMoneyAmountPerLevel;

    public int upgrade4BeginMoney;
    public int upgrade4IncreasingMoneyAmountPerLevel;

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

    internal GameObject upgrade3GameObject;
    internal TextMeshProUGUI upgarede3LevelText;
    internal TextMeshProUGUI upgrade3MoneyText;
    internal Button upgrade3Button;
    internal Button upgrade3ButtonAds;

    internal GameObject point1, point2, point3;

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
        SetUpgrade3UpgradeSystem();
    }

    void SetUpgrade1UpgradeSystem()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= upgrade1BeginMoney)
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
        if (moneytext >= upgrade2BeginMoney)
        {
            upgrade2Button.interactable = true;
        }
        else
        {
            upgrade2Button.interactable = false;
        }
    }

    void SetUpgrade3UpgradeSystem()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= upgrade3BeginMoney)
        {
            upgrade3Button.interactable = true;
        }
        else
        {
            upgrade3Button.interactable = false;
        }
    }

    internal void SetMoney(float number)
    {
        MoneyManager.Instance.IncreaseMoneyAndWrite(-number);
    }

    internal virtual void InitObjects()
    {
        point1 = transform.Find("Point1").gameObject;
        point2 = transform.Find("Point2").gameObject;
        point3 = transform.Find("Point3").gameObject;

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
        upgrade2MoneyText = transform.Find("Upgrade2").Find("NoAds").transform.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        // Upgrade 3
        upgrade3GameObject = transform.Find("Upgrade3").gameObject;
        upgrade3Button = upgrade3GameObject.transform.Find("NoAds").GetComponent<Button>();
        upgrade3ButtonAds = upgrade3GameObject.transform.Find("Ads").GetComponent<Button>();
        upgarede3LevelText = transform.Find("Upgrade3").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade3MoneyText = transform.Find("Upgrade3").transform.Find("NoAds").Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        ConfigureInitializedObjects();
    }

    void ConfigureInitializedObjects()
    {
        //Set Level Text
        if (PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade, 1);
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade).ToString();
        }
        else
        {
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade).ToString();
        }

        if (PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade, 1);
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade).ToString();
        }
        else
        {
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade).ToString();
        }

        if (PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonBowlingUpgrade, 1);
            upgarede3LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade).ToString();
        }
        else
        {
            upgarede3LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade).ToString();
        }

        //Set Money Text

        upgrade1MoneyText.text = upgrade1BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
        upgrade2MoneyText.text = upgrade2BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
        upgrade3MoneyText.text = upgrade3BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
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
            case 3:
                upgrade1GameObject.SetActive(true);
                upgrade2GameObject.SetActive(true);
                upgrade3GameObject.SetActive(true);
                break;
            case 4:
                upgrade1GameObject.SetActive(true);
                upgrade2GameObject.SetActive(true);
                upgrade3GameObject.SetActive(true);
                break;
        }
    }
}
