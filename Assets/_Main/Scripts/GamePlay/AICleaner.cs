using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI.ProceduralImage;

public enum CurrentSituation
{
    goGetShoes, gettingShoes, goSetShoes, settingShoes
}

public class AICleaner : MonoBehaviour
{
    CurrentSituation currentSituation =  CurrentSituation.goGetShoes;
    internal List<GameObject> stackingShoesList = new List<GameObject>();
    public int maxShoesCount = 3;
    float lastCollectTime = 0;

    NavMeshAgent navMeshAgent;

    Transform getShoesTransform;
    Transform setShoesTransform;
    Animator anim;
    internal Transform shoesStackPoint;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        getShoesTransform = GameObject.Find("CleaningArea").transform.Find("GetShoesTransform").transform;
        setShoesTransform = GameObject.Find("CleaningArea").transform.Find("SetShoesTransform").transform;
        shoesStackPoint = transform.Find("ShoesStackPoint").transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetFloat("speed", navMeshAgent.velocity.magnitude);
        Work();
        SetSituation();
    }

    private void SetSituation()
    {
        if(stackingShoesList.Count == maxShoesCount)
        {
            currentSituation = CurrentSituation.goSetShoes;
        }

        if (stackingShoesList.Count == 0)
        {
            currentSituation = CurrentSituation.goGetShoes;
        }
    }

    private void Work()
    {
        switch (currentSituation)
        {
            case CurrentSituation.goGetShoes:
                navMeshAgent.SetDestination(getShoesTransform.position);
                break;
            case CurrentSituation.gettingShoes:
                break;
            case CurrentSituation.goSetShoes:
                navMeshAgent.SetDestination(setShoesTransform.position);
                break;
            case CurrentSituation.settingShoes:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case Tags.DropShoes:
                if (stackingShoesList.Count <= 0 || Time.time - lastCollectTime < 0.1f)
                    break;

                Color cDolor = other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color;
                cDolor.a = 200f / 255f;
                other.transform.Find("Canvas").Find("ImageBg").GetComponent<ProceduralImage>().color = cDolor;

                DropTheShoes(other, stackingShoesList[stackingShoesList.Count - 1]);
                break;
            case Tags.CollectShoes:
                if (Time.fixedTime - lastCollectTime > 0.3f && stackingShoesList.Count <= maxShoesCount)
                {
                    if (stackingShoesList.Count < maxShoesCount)
                    {
                        other.GetComponent<ShoesDistrubutor>().GetShoes(this);
                        lastCollectTime = Time.fixedTime;
                    }
                }
                break;
        }
    }

    void DropTheShoes(Collider other, GameObject shoes)
    {
        if (other.GetComponent<DropShoes>().shoes.Count == 20)
            return;
        lastCollectTime = Time.time;
        stackingShoesList.Remove(shoes);
        shoes.transform.DOMove(other.transform.Find("ShoesPoint").GetChild(other.GetComponent<DropShoes>().shoes.Count).transform.position, 0.4f);
        shoes.transform.DORotate(other.transform.Find("ShoesPoint").GetChild(other.GetComponent<DropShoes>().shoes.Count).transform.eulerAngles, 0.3f);
        StartCoroutine(SetTrue(other, other.GetComponent<DropShoes>().shoes.Count, shoes));
        other.GetComponent<DropShoes>().shoes.Add(other.transform.Find("ShoesPoint").GetChild(other.GetComponent<DropShoes>().shoes.Count).gameObject);
    }

    IEnumerator SetTrue(Collider other, int index, GameObject shoes)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(shoes);
        other.transform.Find("ShoesPoint").GetChild(index).gameObject.SetActive(true);
    }
}