using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VipMiniGameBaseManager : MonoBehaviour
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
    Image upgrade1Image;
    Image upgrade1MoneyImage;
    Image upgrade1MoneyBg;

    internal GameObject upgrade2GameObject;
    internal TextMeshProUGUI upgarede2LevelText;
    internal TextMeshProUGUI upgrade2MoneyText;
    internal Button upgrade2Button;
    Image upgrade2Image;
    Image upgrade2MoneyImage;
    Image upgrade2MoneyBg;

    internal GameObject middlePoint;

    internal virtual void Start()
    {
        InitObjects();
        SetUpgradeSystem();
        SetActiveUpgradeButtons();
    }

    internal void SetUpgradeSystem()
    {
        SetFoosBallArea();
        SetBillardArea();
    }

    void SetFoosBallArea()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= upgrade1BeginMoney)
        {
            ColorBlock colors = upgrade1Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade1Button.colors = colors;
            upgrade1GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            upgrade1Image.color = new Color(1, 1, 1, 1);
            upgrade1MoneyImage.color = new Color(1, 1, 1, 1);
            upgrade1MoneyBg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            ColorBlock colors = upgrade1Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade1Button.colors = colors;
            //upgrade1GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            //upgrade1Image.color = new Color(1, 1, 1, 0.6f);
            upgrade1MoneyImage.color = new Color(1, 1, 1, 0.6f);
            upgrade1MoneyBg.color = new Color(1, 1, 1, 0.6f);
        }
    }

    void SetBillardArea()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= upgrade2BeginMoney)
        {
            ColorBlock colors = upgrade2Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade2Button.colors = colors;
            upgrade2GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            upgrade2Image.color = new Color(1, 1, 1, 1);
            upgrade2MoneyImage.color = new Color(1, 1, 1, 1);
            upgrade2MoneyBg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            ColorBlock colors = upgrade2Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade2Button.colors = colors;
            //upgrade2GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            //upgrade2Image.color = new Color(1, 1, 1, 0.6f);
            upgrade2MoneyImage.color = new Color(1, 1, 1, 0.6f);
            upgrade2MoneyBg.color = new Color(1, 1, 1, 0.6f);
        }
    }
    internal void SetMoney(float number)
    {
        MoneyManager.Instance.IncreaseMoneyAndWrite(-number);
    }

    internal virtual void InitObjects()
    {
        upgrade1GameObject = transform.Find("Upgrade1").gameObject;
        upgrade1Button = upgrade1GameObject.GetComponent<Button>();
        upgrade1Image = transform.Find("Upgrade1").transform.Find("Image").GetComponent<Image>();
        upgarede1LevelText = transform.Find("Upgrade1").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade1MoneyImage = transform.Find("Upgrade1").transform.Find("Money").GetComponent<Image>();
        upgrade1MoneyBg = transform.Find("Upgrade1").transform.Find("MoneyBg").GetComponent<Image>();
        upgrade1MoneyText = transform.Find("Upgrade1").transform.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        upgrade2GameObject = transform.Find("Upgrade2").gameObject;
        upgrade2Button = upgrade2GameObject.GetComponent<Button>();
        upgrade2Image = transform.Find("Upgrade2").transform.Find("Image").GetComponent<Image>();
        upgarede2LevelText = transform.Find("Upgrade2").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade2MoneyImage = transform.Find("Upgrade2").transform.Find("Money").GetComponent<Image>();
        upgrade2MoneyBg = transform.Find("Upgrade2").transform.Find("MoneyBg").GetComponent<Image>();
        upgrade2MoneyText = transform.Find("Upgrade2").transform.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        middlePoint = transform.Find("Point3").gameObject;

        ConfigureInitializedObjects();
    }

    void ConfigureInitializedObjects()
    {
        //Set Level Text
        if (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonShelfUpgrade, 1);
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonShelfUpgrade).ToString();
        }
        else
        {
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonShelfUpgrade).ToString();
        }

        if (PlayerPrefs.GetInt(Key.ButtonShelfUpgrade) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonShelfUpgrade, 1);
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonShelfUpgrade).ToString();
        }
        else
        {
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonShelfUpgrade).ToString();
        }


        upgrade1MoneyText.text = upgrade1BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
        upgrade2MoneyText.text = upgrade2BeginMoney.ToString(MoneyManager.Instance.moneyFormat);
    }

    void SetActiveUpgradeButtons()
    {
        upgrade1GameObject.SetActive(true);
        upgrade2GameObject.SetActive(true);
    }
}