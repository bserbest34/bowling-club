using UnityEngine;
using DG.Tweening;
using UnityEngine.UI.ProceduralImage;
using TMPro;

public class IdleMoneyCollector : MonoBehaviour
{
    public int needMoneyCount;
    internal int currentMoneyCount = 0;
    ProceduralImage fillImage;
    TextMeshProUGUI textMP;

    void Start()
    {
        fillImage = transform.Find("Canvas").Find("Image").GetComponent<ProceduralImage>();
        textMP = transform.Find("Canvas").Find("TextMP").GetComponent<TextMeshProUGUI>();
    }

    internal void SetMoneyToCollector(GameObject money)
    {
        if (currentMoneyCount >= needMoneyCount)
            return;
        FindObjectOfType<IdleMoneyManager>().stackingMoneyList.Remove(money);
        currentMoneyCount += 1;
        money.transform.parent = null;
        money.transform.DOMove(transform.position, 0.1f);
        Destroy(money, 0.1f);
        fillImage.fillAmount += 1 / (float)needMoneyCount;
        textMP.text = currentMoneyCount + "$" + " / " + needMoneyCount + "$";
    }
}
