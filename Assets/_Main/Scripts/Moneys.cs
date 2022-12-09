using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Moneys : MonoBehaviour
{
    public GameObject moneyPrefab;
    public List<GameObject> moneys = new List<GameObject>();
    List<Vector3> positions = new List<Vector3>();
    internal int openMoneyCount = 0;

    private void Awake()
    {
        foreach (var item in moneys)
        {
            positions.Add(item.transform.position);
        }
    }

    private void Update()
    {
        if(openMoneyCount == 0)
        {
            transform.Find("Cube").GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
        }else if(openMoneyCount < 4)
        {
            transform.Find("Cube").GetComponent<BoxCollider>().size = new Vector3(0.25f, 1, 1);
        }else if(openMoneyCount < 7)
        {
            transform.Find("Cube").GetComponent<BoxCollider>().size = new Vector3(0.5f, 1, 1);
        }else
        {
            transform.Find("Cube").GetComponent<BoxCollider>().size = new Vector3(1f, 1, 1);
        }
    }

    internal void GetMoney()
    {
        if (openMoneyCount <= 0)
            return;
        Vibrations.Soft();
        GameObject temp = moneys[openMoneyCount - 1];
        StartCoroutine(SetFalse(temp));
        MoneyManager.Instance.CreateMoney(1, true, temp.transform.position);
        openMoneyCount--;
    }

    IEnumerator SetFalse(GameObject temp)
    {
        yield return new WaitForSeconds(0.18f);
        temp.SetActive(false);
        temp.transform.position = positions[moneys.IndexOf(temp)];
    }

    internal void SetMoney(int moneyCount, GameObject ai)
    {
        for (int i = 0; i < moneyCount; i++)
        {
            StartCoroutine(SetMoneySlowly(ai));
        }
    }

    internal IEnumerator SetMoneySlowly(GameObject ai)
    {
        if (openMoneyCount < 0)
            openMoneyCount = 0;
        if (openMoneyCount < moneys.Count)
        {
            GameObject temp = moneys[openMoneyCount];
            openMoneyCount++;
            temp.transform.position = ai.transform.position;
            temp.transform.localScale = new Vector3(90, 14f, 30f);
            temp.SetActive(true);
            temp.transform.DOScale(new Vector3(180.4588f, 44.25546f, 101.1079f), 0.4f);
            temp.transform.DOJump(positions[moneys.IndexOf(temp)], 2, 1, 0.4f);
        }
        yield return new WaitForSeconds(0.1f);
    }
}