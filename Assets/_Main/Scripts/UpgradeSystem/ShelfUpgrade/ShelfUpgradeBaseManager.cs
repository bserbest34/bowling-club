using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShelfUpgradeBaseManager : MonoBehaviour
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

    internal GameObject middlePoint;

    internal virtual void Start()
    {
        InitObjects();
        SetUpgradeSystem();
        SetActiveUpgradeButtons();
    }

    internal void SetUpgradeSystem()
    {
        SetUpgrade1();
        SetUpgrade2();
    }

    void SetUpgrade1()
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

    void SetUpgrade2()
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
    internal void SetMoney(float number)
    {
        MoneyManager.Instance.IncreaseMoneyAndWrite(-number);
    }

    internal virtual void InitObjects()
    {
        upgrade1GameObject = transform.Find("Upgrade1").gameObject;
        upgrade1Button = upgrade1GameObject.transform.Find("NoAds").GetComponent<Button>();
        upgrade1ButtonAds = upgrade1GameObject.transform.Find("Ads").GetComponent<Button>();
        upgarede1LevelText = transform.Find("Upgrade1").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade1MoneyText = transform.Find("Upgrade1").transform.Find("NoAds").Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        upgrade2GameObject = transform.Find("Upgrade2").gameObject;
        upgrade2Button = upgrade2GameObject.transform.Find("NoAds").GetComponent<Button>();
        upgrade2ButtonAds = upgrade2GameObject.transform.Find("Ads").GetComponent<Button>();
        upgarede2LevelText = transform.Find("Upgrade2").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade2MoneyText = transform.Find("Upgrade2").transform.Find("NoAds").Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        middlePoint = transform.Find("Point3").gameObject;

        ConfigureInitializedObjects();
    }

    void ConfigureInitializedObjects()
    {
        //Set Level Text
        if (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonShelfUpgrade, 1);
            upgarede1LevelText.text = "LEVEL " + 2;
        }
        else
        {
            upgarede1LevelText.text = "LEVEL " + 2;
        }

        if (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonShelfUpgrade, 1);
            upgarede2LevelText.text = "LEVEL " + 3;
        }
        else
        {
            upgarede2LevelText.text = "LEVEL " + 3;
        }


        upgrade1MoneyText.text = upgrade1BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
        upgrade2MoneyText.text = upgrade2BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
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
