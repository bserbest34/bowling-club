using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using UnityEngine.AI;

public class CleanArea : MonoBehaviour
{
    public List<GameObject> shoesCustomers = new List<GameObject>();

    public float needMoneyCount;
    ProceduralImage fillImage;
    bool isOpenProcess = false;
    CleanArea cleanArea;
    Transform point;
    DailyTaskManager dailyTaskManager;
    DropShoes dropShoes;

    void Start()
    {
        dropShoes = transform.Find("Unlock").Find("DropShoes").GetComponent<DropShoes>();
        dailyTaskManager = FindObjectOfType<DailyTaskManager>();
        fillImage = transform.Find("Unlock").Find("DropShoes").Find("Canvas").Find("Image").GetComponent<ProceduralImage>();
        point = transform.Find("Unlock").Find("Point").transform;

        cleanArea = FindObjectOfType<CleanArea>();
    }

    private void Update()
    {
        if(IsGiveableShoes())
        {
            SetShoesCustomers();
        }
    }

    internal bool IsGiveableShoes()
    {
        if (cleanArea.shoesCustomers.Count <= 0 || dropShoes.shoes.Count <= 0)
            return false;

        if (Vector3.Distance(transform.Find("Obstacle2").position, cleanArea.shoesCustomers[0].transform.position) < 10f)
        {
            return true;
        }
        return false;
    }

    internal void SetShoesCustomers(bool isMainChar = false)
    {
        if (isOpenProcess)
            return;

        isOpenProcess = true;
        if(isMainChar)
            Vibrations.Soft();
        if (cleanArea.shoesCustomers.Count > 0)
            StartCoroutine(SetFill());
        else
            StartCoroutine(SetFalse());
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
        isOpenProcess = false;

        if (shoesCustomers.Count > 0)
        {
            dropShoes.GiveShoesToCustomer();
            shoesCustomers[0].GetComponent<NavMeshAgent>().SetDestination(point.position);
            shoesCustomers.RemoveAt(0);

            if (dailyTaskManager != null)
            {
                dailyTaskManager.SetValue(Missions.distrubShoes, 1);
            }
        }
    }
}