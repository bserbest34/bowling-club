using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class UpgradeArea : MonoBehaviour
{
    public float needMoneyCount;
    public int moneyPlus;
    bool isOpenProcess = false;
    int level;
    ProceduralImage fillImage;
    TextMeshProUGUI textMP;
    JoystickControl joystickControl;
    BallCollector ballCollector;

    Color mainColor;

    void Start()
    {
        needMoneyCount = PlayerPrefs.GetFloat("NeedMoneyCount" + transform.name, needMoneyCount);
        level = PlayerPrefs.GetInt("Level" + transform.name, level);
        joystickControl = FindObjectOfType<JoystickControl>();
        ballCollector = FindObjectOfType<BallCollector>();
        fillImage = transform.Find("Canvas").Find("Image").GetComponent<ProceduralImage>();
        textMP = transform.Find("Canvas").Find("TextMP").GetComponent<TextMeshProUGUI>();
        textMP.text = needMoneyCount + "$";

        mainColor = transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;

    }

    internal void SetMoneyToUpgradeArea()
    {
        if (isOpenProcess)
            return;

        isOpenProcess = true;
        if (MoneyManager.Instance.money >= needMoneyCount)
        {
            MoneyManager.Instance.IncreaseMoneyAndWrite(-needMoneyCount);
            FindObjectOfType<BallCollector>().InstantiateMoney();
            StartCoroutine(SetFill());
        }else
        {
            transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = Color.red;
            StartCoroutine(SetFalse());
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(1f);
        transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = mainColor;
        isOpenProcess = false;
    }

    IEnumerator SetFill()
    {
        float velocity = 0f;
        while (fillImage.fillAmount < 1)
        {
            yield return new WaitForSeconds(0);
            fillImage.fillAmount = Mathf.SmoothDamp(fillImage.fillAmount, 1, ref velocity, Time.deltaTime * 0.1f, 10);
        }

        fillImage.fillAmount = 0;
        needMoneyCount += moneyPlus;
        textMP.text = needMoneyCount + "$";

        PlayerPrefs.SetFloat("NeedMoneyCount" + transform.name, needMoneyCount);
        PlayerPrefs.SetInt("Level" + transform.name, level + 1);

        switch (tag)
        {
            case Tags.SpeedUpgrade:
                joystickControl.SpeedUpgrade();
                break;
            case Tags.StackUpgrade:
                ballCollector.StackUpgrade();
                break;
        }
        isOpenProcess = false;
    }
}