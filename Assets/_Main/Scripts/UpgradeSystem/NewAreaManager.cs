using UnityEngine;

//Notes:
// PlayerPrefs.GetInt(Key.Button1_Level)
public class NewAreaManager : NewAreaBaseManager
{
    BallCollector ballCollector;
    JoystickControl joystickControl;
    AreaManager areaManager;

    int billarArea = 0;
    int foosBallArea = 0;

    internal override void Start()
    {
        billarArea = PlayerPrefs.GetInt(Key.ButtonBillard + transform.root.GetChild(2).name, 0);
        foosBallArea = PlayerPrefs.GetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name, 0);
        ballCollector = FindObjectOfType<BallCollector>();
        joystickControl = FindObjectOfType<JoystickControl>();
        areaManager = FindObjectOfType<AreaManager>();
        base.Start();
        CloseButtons();
        CheckBillardArea();
        CheckFoosBallArea();
        //UIManager.OnStart += CloseButtons;
    }
    void OnClickBuyFoosBall()
    {
        if (PlayerPrefs.GetFloat(Key.Money) < int.Parse(upgrade1MoneyText.text)) return;
        PlayerPrefs.SetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name, PlayerPrefs.GetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name) + 1);
        SetMoney(PlayerPrefs.GetFloat(Key.ButtonFoosballMoney));
        PlayerPrefs.SetFloat(Key.ButtonFoosballMoney, (PlayerPrefs.GetFloat(Key.ButtonFoosballMoney) + upgrade1IncreasingMoneyAmountPerLevel));

        upgrade1MoneyText.text = PlayerPrefs.GetFloat(Key.ButtonFoosballMoney).ToString(MoneyManager.Instance.moneyFormat);
        upgarede1LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name).ToString();

        SetUpgradeSystem();
        OpenFoosBallArea();
    }

    void OnClickBuyBillard()
    {
        if (PlayerPrefs.GetFloat(Key.Money) < int.Parse(upgrade2MoneyText.text)) return;
        PlayerPrefs.SetInt(Key.ButtonBillard + transform.root.GetChild(2).name, PlayerPrefs.GetInt(Key.ButtonBillard + transform.root.GetChild(2).name) + 1);
        SetMoney(PlayerPrefs.GetFloat(Key.ButtonBillarMoney));
        PlayerPrefs.SetFloat(Key.ButtonBillarMoney, (PlayerPrefs.GetFloat(Key.ButtonBillarMoney) + upgrade2IncreasingMoneyAmountPerLevel));

        upgrade2MoneyText.text = PlayerPrefs.GetFloat(Key.ButtonBillarMoney).ToString(MoneyManager.Instance.moneyFormat);
        upgarede2LevelText.text = "LVL " + PlayerPrefs.GetInt(Key.ButtonBillard + transform.root.GetChild(2).name).ToString();

        SetUpgradeSystem();
        OpenBillardArea();
    }


    internal override void InitObjects()
    {
        base.InitObjects();
        upgrade1Button.onClick.AddListener(OnClickBuyFoosBall);
        upgrade2Button.onClick.AddListener(OnClickBuyBillard);
    }

    internal void CloseButtons()
    {
        transform.parent.GetComponent<Canvas>().sortingOrder = 0;
        if (upgrade1GameObject != null)
            upgrade1GameObject.SetActive(false);
        if (upgrade2GameObject != null)
            upgrade2GameObject.SetActive(false);
    }
    internal void OpenButtons()
    {
        SetUpgradeSystem();
        transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        if (upgrade1GameObject != null)
            upgrade1GameObject.SetActive(true);
        if (upgrade2GameObject != null)
            upgrade2GameObject.SetActive(true);
    }

    void OpenBillardArea()
    {
        PlayerPrefs.SetInt(Key.ButtonBillard + transform.root.GetChild(2).name, billarArea + 1);
        billarArea = PlayerPrefs.GetInt(Key.ButtonBillard + transform.root.GetChild(2).name);

        transform.root.GetChild(2).gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        transform.root.Find("Canvas").gameObject.SetActive(false);
        areaManager.billardAreas.Add(transform.root.GetChild(2).gameObject);
    }
    void CheckBillardArea()
    {
        //Start'a calisan.
        switch (PlayerPrefs.GetInt(Key.ButtonBillard + transform.root.GetChild(2).name))
        {
            case 1:
                transform.root.GetChild(2).gameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                transform.root.Find("Canvas").gameObject.SetActive(false);
                areaManager.billardAreas.Add(transform.root.GetChild(2).gameObject);
                break;
        }
    }
    void OpenFoosBallArea()
    {
        PlayerPrefs.SetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name, billarArea + 1);
        foosBallArea = PlayerPrefs.GetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name);

        
        transform.root.GetChild(3).gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        transform.root.Find("Canvas").gameObject.SetActive(false);
        areaManager.langertAreas.Add(transform.root.GetChild(3).gameObject);
    }
    void CheckFoosBallArea()
    {
        //Start'a calisan.
        switch (PlayerPrefs.GetInt(Key.ButtonFoosBall + transform.root.GetChild(3).name))
        {
            case 1:
                transform.root.GetChild(3).gameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                transform.root.Find("Canvas").gameObject.SetActive(false);
                areaManager.langertAreas.Add(transform.root.GetChild(3).gameObject);
                break;
        }
    }
}