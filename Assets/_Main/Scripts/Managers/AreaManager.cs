using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    CleanArea cleanAreaScript;
    public GameObject CollectorAI;

    public List<GameObject> bowlingAreas = new List<GameObject>();
    public List<GameObject> langertAreas = new List<GameObject>();
    public List<GameObject> billardAreas = new List<GameObject>();
    public List<GameObject> cafeAreas = new List<GameObject>();
    public List<GameObject> cleanArea = new List<GameObject>();
    public List<GameObject> dartAreas = new List<GameObject>();
    public List<GameObject> miniCafeArea = new List<GameObject>();
    public List<GameObject> tableTennisAreas = new List<GameObject>();
    public List<GameObject> arcadeMachineArea = new List<GameObject>();

    private void Start()
    {
        cleanAreaScript = FindObjectOfType<CleanArea>();
    }

    void BowlingCustomer()
    {
        foreach (var item in bowlingAreas)
        {
            if (item.GetComponent<BowlingArea>().maxCount > item.GetComponent<BowlingArea>().currentCustomerCount)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        if (cleanArea.Count == 1)
                        {
                            ai.GetComponent<AINavMesh>().areaType = AreaType.cleanShoes;
                            cleanAreaScript.shoesCustomers.Add(ai);
                            item.GetComponent<BowlingArea>().currentCustomerCount++;
                            return;
                        }
                        else if (cleanArea.Count == 0)
                        {
                            ai.GetComponent<AINavMesh>().areaType = AreaType.bowling;
                            item.GetComponent<BowlingArea>().currentCustomerCount++;
                            return;
                        }
                    }
                }
            }
        }
    }
    private void Update()
    {
        BowlingCustomer();
        MiniCafeCustomer();
        TennisCustomer();

        foreach (var item in dartAreas)
        {
            if(item.GetComponent<DartArea>().currentCustomer < 1)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if(ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.dart;
                        item.GetComponent<DartArea>().dartCustomers.Add(ai);
                        item.GetComponent<DartArea>().currentCustomer++;
                        return;
                    }
                }
            }
        }

        foreach (var item in arcadeMachineArea)
        {
            if (item.GetComponent<ArcadeArea>().currentCustomer < 1)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.arcadeMachine;
                        item.GetComponent<ArcadeArea>().dartCustomers.Add(ai);
                        item.GetComponent<ArcadeArea>().currentCustomer++;
                        return;
                    }
                }
            }
        }

        foreach (var item in langertAreas)
        {
            if (item.GetComponent<LangertArea>().currentCustomer <= 1)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.langert;
                        item.GetComponent<LangertArea>().langertCustomers.Add(ai);
                        item.GetComponent<LangertArea>().currentCustomer ++;
                        return;
                    }
                }
            }
        }

        foreach (var item in billardAreas)
        {
            if (item.GetComponent<BillardArea>().currentCustomer <= 1)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.bilardo;
                        item.GetComponent<BillardArea>().billardCustormers.Add(ai);
                        item.GetComponent<BillardArea>().currentCustomer++;
                        return;
                    }
                }
            }
        }

        foreach (var item in cafeAreas)
        {
            if (item.GetComponent<CafeArea>().currentCafeCustomer <= 0)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.kafeterya;
                        item.GetComponent<CafeArea>().cafeCustomers.Add(ai);
                        item.GetComponent<CafeArea>().currentCafeCustomer++;
                        return;
                    }
                }
            }
        }

        foreach (var item in cafeAreas)
        {
            if (item.GetComponent<CafeArea>().currentCafeChair1 <= 0)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.kafeterya;
                        item.GetComponent<CafeArea>().cafeChair1.Add(ai);
                        item.GetComponent<CafeArea>().currentCafeChair1++;
                        return;
                    }
                }
            }
        }

        foreach (var item in cafeAreas)
        {
            if (item.GetComponent<CafeArea>().currentCafeChair2 <= 0)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.kafeterya;
                        item.GetComponent<CafeArea>().cafeChair2.Add(ai);
                        item.GetComponent<CafeArea>().currentCafeChair2++;
                        return;
                    }
                }
            }
        }

        foreach (var item in cafeAreas)
        {
            if (item.GetComponent<CafeArea>().currentCafeChair3 <= 0)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.kafeterya;
                        item.GetComponent<CafeArea>().cafeChair3.Add(ai);
                        item.GetComponent<CafeArea>().currentCafeChair3++;
                        return;
                    }
                }
            }
        }
    }

    void TennisCustomer()
    {
        foreach (var item in tableTennisAreas)
        {
            if (item.GetComponent<TableTennis>().maxCustomer > item.GetComponent<TableTennis>().currentCustomer)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.tableTennis;
                        item.GetComponent<TableTennis>().tableTennisCustomers.Add(ai);
                        item.GetComponent<TableTennis>().currentCustomer++;
                        return;
                    }
                }
            }
        }
    }
    void MiniCafeCustomer()
    {
        foreach (var item in miniCafeArea)
        {
            if (item.GetComponent<MiniCafeArea>().maxCustomerCount > item.GetComponent<MiniCafeArea>().currentCafeCustomer)
            {
                foreach (var ai in GameObject.FindGameObjectsWithTag("Customer"))
                {
                    if (ai.GetComponent<AINavMesh>().currentArea == null && ai.GetComponent<AINavMesh>().isPlayable)
                    {
                        ai.GetComponent<AINavMesh>().currentArea = item;
                        ai.GetComponent<AINavMesh>().areaType = AreaType.miniCafe;
                        item.GetComponent<MiniCafeArea>().cafeCustomers.Add(ai);
                        item.GetComponent<MiniCafeArea>().currentCafeCustomer++;
                    }
                }
            }
        }
    }
}