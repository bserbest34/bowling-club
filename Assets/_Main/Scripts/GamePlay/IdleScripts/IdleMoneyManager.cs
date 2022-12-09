using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IdleMoneyManager : MonoBehaviour
{
    GameObject moneyStackpoint;
    internal List<GameObject> stackingMoneyList = new List<GameObject>();
    public int maxMoneyCount = 15;
    float lastCollectTime;

    void Start()
    {
        moneyStackpoint = transform.Find("MoneyStackPoint").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Money"))
        {
            if (stackingMoneyList.Count >= maxMoneyCount)
                return;
            other.enabled = false;
            SetMoneyPosition(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MoneyCollector"))
        {
            if (stackingMoneyList.Count <= 0 || Time.time - lastCollectTime < 0.1f)
                return;
            lastCollectTime = Time.time;
            other.GetComponent<IdleMoneyCollector>().SetMoneyToCollector(stackingMoneyList[stackingMoneyList.Count - 1]);
        }
    }

    void SetMoneyPosition(Collider other)
    {
        if (stackingMoneyList.Count >= maxMoneyCount)
            return;
        other.enabled = false;
        other.gameObject.transform.parent = moneyStackpoint.transform;
        stackingMoneyList.Add(other.gameObject);
        other.transform.DOLocalRotate(new Vector3(0, 90, 0), 0.25f);
        other.transform.DOLocalJump(new Vector3(0,
            moneyStackpoint.transform.position.y + (4.5f * stackingMoneyList.Count), 0), 10, 1 ,0.25f);
    }
}