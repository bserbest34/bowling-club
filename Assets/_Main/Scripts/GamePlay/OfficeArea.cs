using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class OfficeArea : MonoBehaviour
{
    public float needMoneyCount;
    ProceduralImage fillImage;
    ProceduralImage bgImage;
    Color bgImageMainColor;
    TextMeshProUGUI textMP;
    bool isOpenProcess = false;
    CustomerManager customerManager;
    BallCollector ballCollector;

    void Start()
    {
        bgImage = transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>();
        fillImage = transform.Find("Canvas").Find("Image").GetComponent<ProceduralImage>();
        textMP = transform.Find("Canvas").Find("TextMP").GetComponent<TextMeshProUGUI>();
        bgImageMainColor = bgImage.color;

        customerManager = FindObjectOfType<CustomerManager>();
        ballCollector = FindObjectOfType<BallCollector>();
    }

    private void Update()
    {
        if(ballCollector.saloonCapasity > customerManager.customers.Count)
        {
            textMP.gameObject.SetActive(false);
            bgImage.color = bgImageMainColor;
        }
    }
    internal void SetNewCustomer()
    {
        if (isOpenProcess)
            return;

        isOpenProcess = true;
        if (ballCollector.saloonCapasity > customerManager.customers.Count)
        {
            StartCoroutine(SetFill());
        }
        else
        {
            textMP.gameObject.SetActive(true);
            bgImage.color = Color.red;
            StartCoroutine(SetFalse());
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(1f);
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

        if(customerManager.waiters.Count > 0)
        {
            transform.Find("Monies").GetComponent<Moneys>().SetMoney(3, customerManager.waiters[0]);
            customerManager.waiters[0].tag = Tags.Customer;
            customerManager.customers.Add(customerManager.waiters[0].gameObject);
            customerManager.waiters.RemoveAt(0);
        }
        customerManager.InstatiateAIs();
        customerManager.AISirayaGirme();
        isOpenProcess = false;
    }
}