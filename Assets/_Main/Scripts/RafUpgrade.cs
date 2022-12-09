using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class RafUpgrade : MonoBehaviour
{
    public float needMoneyCount;
    ProceduralImage fillImage;
    TextMeshProUGUI textMP;
    bool isOpenProcess = false;
    int upgradeCount;
    public int upgradeTwoCount;
    public int upgradeThreeCount;

    Color mainColor;

    void Start()
    {
        upgradeCount = 0;
        upgradeCount = PlayerPrefs.GetInt("RafUpgrade", 0);
        fillImage = transform.Find("Canvas").Find("Image").GetComponent<ProceduralImage>();
        textMP = transform.Find("Canvas").Find("TextMP").GetComponent<TextMeshProUGUI>();
        textMP.text = needMoneyCount + "$";
        RafBallOpen();
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
        }
        else
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
        switch (PlayerPrefs.GetInt("RafUpgrade"))
        {
            case 0:
                break;
            case 1:
                needMoneyCount = upgradeTwoCount;
                textMP.text = needMoneyCount + "$";
                break;
            case 2:
                needMoneyCount = upgradeThreeCount;
                textMP.text = needMoneyCount + "$";
                break;
        }


        switch (tag)
        {
            case Tags.UpgradeArea:
                RafUpgradeCount();
                RafBallOpen();
                break;
        }


        isOpenProcess = false;
    }

    void RafUpgradeCount()
    {
        PlayerPrefs.SetInt("RafUpgrade", upgradeCount + 1);
        upgradeCount = PlayerPrefs.GetInt("RafUpgrade");
    }

    internal void RafBallOpen()
    {
        switch (PlayerPrefs.GetInt("RafUpgrade"))
        {
            case 0:
                break;
            case 1:
                needMoneyCount = upgradeTwoCount;
                textMP.text = needMoneyCount + "$";
                transform.parent.Find("ShelfLvl1").gameObject.SetActive(false);
                transform.parent.Find("BallsLevel1").gameObject.SetActive(false);
                transform.parent.Find("ShelfsLvl2").gameObject.SetActive(true);
                transform.parent.Find("BallsLeftLevel2").gameObject.SetActive(true);
                transform.parent.Find("BallsRightLevel2").gameObject.SetActive(true);
                transform.parent.GetComponent<BallDistributorManager>().rafBalls.Clear();
                for (int i = 0; i < transform.parent.Find("BallsRightLevel2").childCount; i++)
                {
                    transform.parent.GetComponent<BallDistributorManager>().rafBalls.Add(transform.parent.Find("BallsRightLevel2").GetChild(i).gameObject);
                }
                for (int i = 0; i < transform.parent.Find("BallsLeftLevel2").childCount; i++)
                {
                    transform.parent.GetComponent<BallDistributorManager>().rafBalls.Add(transform.parent.Find("BallsLeftLevel2").GetChild(i).gameObject);
                }
                transform.localPosition = new Vector3(-8f, 1.88f, 1.99f);
                break;
            case 2:
                needMoneyCount = upgradeThreeCount;
                textMP.text = needMoneyCount + "$";
                transform.parent.Find("ShelfLvl1").gameObject.SetActive(false);
                transform.parent.Find("BallsLevel1").gameObject.SetActive(false);
                transform.parent.Find("ShelfsLvl2").gameObject.SetActive(false);
                transform.parent.Find("BallsLeftLevel2").gameObject.SetActive(false);
                transform.parent.Find("BallsRightLevel2").gameObject.SetActive(false);
                transform.parent.Find("ShelfLvl3").gameObject.SetActive(true);
                transform.parent.Find("BallsLeftLevel3").gameObject.SetActive(true);
                transform.parent.Find("BallsRightLevel3").gameObject.SetActive(true);
                transform.parent.GetComponent<BallDistributorManager>().rafBalls.Clear();
                for (int i = 0; i < transform.parent.Find("BallsRightLevel3").childCount; i++)
                {
                    transform.parent.GetComponent<BallDistributorManager>().rafBalls.Add(transform.parent.Find("BallsRightLevel3").GetChild(i).gameObject);
                }
                for (int i = 0; i < transform.parent.Find("BallsLeftLevel3").childCount; i++)
                {
                    transform.parent.GetComponent<BallDistributorManager>().rafBalls.Add(transform.parent.Find("BallsLeftLevel3").GetChild(i).gameObject);
                }
                transform.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}