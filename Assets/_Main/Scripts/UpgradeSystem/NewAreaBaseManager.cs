using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewAreaBaseManager : MonoBehaviour
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

    internal GameObject upgrade2GameObject;
    internal TextMeshProUGUI upgarede2LevelText;
    internal TextMeshProUGUI upgrade2MoneyText;
    internal Button upgrade2Button;
    Image upgrade2Image;
    Image upgrade2MoneyImage;

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
        if (moneytext >= PlayerPrefs.GetFloat(Key.ButtonFoosballMoney))
        {
            ColorBlock colors = upgrade1Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade1Button.colors = colors;
            upgrade1GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            upgrade1Image.color = new Color(1, 1, 1, 1);
            upgrade1MoneyImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            ColorBlock colors = upgrade1Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade1Button.colors = colors;
            upgrade1GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            upgrade1Image.color = new Color(1, 1, 1, 0.6f);
            upgrade1MoneyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }

    void SetBillardArea()
    {
        float moneytext = PlayerPrefs.GetFloat(Key.Money);
        if (moneytext >= PlayerPrefs.GetFloat(Key.ButtonBillarMoney))
        {
            ColorBlock colors = upgrade2Button.colors;
            colors.pressedColor = new Color(0.76f, 0.76f, 0.76f, 1);
            upgrade2Button.colors = colors;
            upgrade2GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            upgrade2Image.color = new Color(1, 1, 1, 1);
            upgrade2MoneyImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            ColorBlock colors = upgrade2Button.colors;
            colors.pressedColor = new Color(1, 1, 1, 1);
            upgrade2Button.colors = colors;
            upgrade2GameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            upgrade2Image.color = new Color(1, 1, 1, 0.6f);
            upgrade2MoneyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }
    internal void SetMoney(float number)
    {
        MoneyManager.Instance.IncreaseMoneyAndWrite(-number);
    }

    internal virtual void InitObjects()
    {
        // FoosBall
        upgrade1GameObject = transform.Find("Upgrade1").gameObject;
        upgrade1Button = upgrade1GameObject.GetComponent<Button>();
        upgrade1Image = transform.Find("Upgrade1").transform.Find("Image").GetComponent<Image>();
        upgarede1LevelText = transform.Find("Upgrade1").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade1MoneyImage = transform.Find("Upgrade1").transform.Find("Money").GetComponent<Image>();
        upgrade1MoneyText = transform.Find("Upgrade1").transform.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        // Billard
        upgrade2GameObject = transform.Find("Upgrade2").gameObject;
        upgrade2Button = upgrade2GameObject.GetComponent<Button>();
        upgrade2Image = transform.Find("Upgrade2").transform.Find("Image").GetComponent<Image>();
        upgarede2LevelText = transform.Find("Upgrade2").transform.Find("Level").GetComponent<TextMeshProUGUI>();
        upgrade2MoneyImage = transform.Find("Upgrade2").transform.Find("Money").GetComponent<Image>();
        upgrade2MoneyText = transform.Find("Upgrade2").transform.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();

        ConfigureInitializedObjects();
    }

    void ConfigureInitializedObjects()
    {
        //Set Level Text
        if (PlayerPrefs.GetInt(Key.ButtonFoosBall) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonFoosBall, 1);
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonFoosBall).ToString();
        }
        else
        {
            upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonFoosBall).ToString();
        }

        if (PlayerPrefs.GetInt(Key.ButtonBillard) == 0)
        {
            PlayerPrefs.SetInt(Key.ButtonBillard, 1);
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBillard).ToString();
        }
        else
        {
            upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBillard).ToString();
        }

        //Set Money Text
        if (PlayerPrefs.GetFloat(Key.ButtonFoosballMoney) == 0)
        {
            PlayerPrefs.SetFloat(Key.ButtonFoosballMoney, upgrade1BeginMoney);
            upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.ButtonFoosballMoney).ToString(MoneyManager.Instance.moneyFormat);
        }
        else
        {
            upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.ButtonFoosballMoney).ToString(MoneyManager.Instance.moneyFormat);
        }

        if (PlayerPrefs.GetFloat(Key.ButtonBillarMoney) == 0)
        {
            PlayerPrefs.SetFloat(Key.ButtonBillarMoney, upgrade2BeginMoney);
            upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.ButtonBillarMoney).ToString(MoneyManager.Instance.moneyFormat);
        }
        else
        {
            upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.ButtonBillarMoney).ToString(MoneyManager.Instance.moneyFormat);
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
