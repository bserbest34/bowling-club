using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShoesDistrubutor : MonoBehaviour
{
    [SerializeField] internal List<GameObject> rafShoes = new List<GameObject>();

    BallCollector ballCollector;
    float lastInstantiateTime = 0;
    internal float instantFreq = 7;
    public List<GameObject> shoesLevels = new List<GameObject>();
    internal int level = 0;

    private void Awake()
    {
        ballCollector = FindObjectOfType<BallCollector>();
    }

    private void Update()
    {
        if (Time.time - lastInstantiateTime < (instantFreq - (PlayerPrefs.GetInt(Key.ShoesUpgrade + transform.root.name) * 1.25f))) return;
        InstantiateShoes();
    }

    void InstantiateShoes()
    {
        GameObject temp;
        int lastOpenCollectionIndex = 1;

        switch (level)
        {
            case 0:
                if (rafShoes.Count == 9) break;
                temp = Instantiate(shoesLevels[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Unlock").Find("Lvl1Shoes").GetChild(9 - rafShoes.Count - 1).transform.position,
                    Quaternion.identity, transform.root.Find("Unlock").Find("Lvl1ShoesNew"));
                rafShoes.Add(temp);
                temp.transform.parent = transform.root.Find("Unlock").Find("Lvl1ShoesNew");
                temp.SetActive(true);
                lastInstantiateTime = Time.time;
                break;
            case 1:
                if (rafShoes.Count == 9) break;
                temp = Instantiate(shoesLevels[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Unlock").Find("Lvl2Shoes").GetChild(9 - rafShoes.Count - 1).transform.position,
                    Quaternion.identity, transform.root.Find("Unlock").Find("Lvl2ShoesNew"));
                rafShoes.Add(temp);
                temp.transform.parent = transform.root.Find("Unlock").Find("Lvl2ShoesNew");
                temp.SetActive(true);
                lastInstantiateTime = Time.time;
                break;
            case 2:
                if (rafShoes.Count == 9) break;
                temp = Instantiate(shoesLevels[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Unlock").Find("Lvl3Shoes").GetChild(9 - rafShoes.Count - 1).transform.position,
                    Quaternion.identity, transform.root.Find("Unlock").Find("Lvl3ShoesNew"));
                rafShoes.Add(temp);
                temp.transform.parent = transform.root.Find("Unlock").Find("Lvl3ShoesNew");
                temp.SetActive(true);
                lastInstantiateTime = Time.time;
                break;
        }
    }

    internal void SetFirstShoes()
    {
        int lastOpenCollectionIndex = 1;

        switch (level)
        {
            case 0:
                rafShoes.Clear();
                transform.root.Find("Unlock").Find("Lvl1ShoesShelf").gameObject.SetActive(true);
                for (int i = 0; i < 9; i++)
                {
                    GameObject temp = Instantiate(shoesLevels[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Unlock").Find("Lvl1Shoes").GetChild(8 - i).transform.position,
                        Quaternion.identity, transform.root.Find("Unlock").Find("Lvl1ShoesNew"));
                    rafShoes.Add(temp);
                    temp.SetActive(true);
                }
                break;
            case 1:
                rafShoes.Clear();
                transform.root.Find("Unlock").Find("Lvl1ShoesShelf").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl2ShoesShelf").gameObject.SetActive(true);
                for (int i = 0; i < 9; i++)
                {
                    GameObject temp = Instantiate(shoesLevels[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Unlock").Find("Lvl2Shoes").GetChild(8 - i).transform.position,
                        Quaternion.identity, transform.root.Find("Unlock").Find("Lvl2ShoesNew"));
                    rafShoes.Add(temp);
                    temp.SetActive(true);
                }
                break;
            case 2:
                rafShoes.Clear();
                transform.root.Find("Unlock").Find("Lvl1ShoesShelf").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl2ShoesShelf").gameObject.SetActive(false);
                transform.root.Find("Unlock").Find("Lvl3ShoesShelf").gameObject.SetActive(true);
                for (int i = 0; i < 9; i++)
                {
                    GameObject temp = Instantiate(shoesLevels[Random.Range(0, lastOpenCollectionIndex)], transform.root.Find("Unlock").Find("Lvl3Shoes").GetChild(8 - i).transform.position,
                        Quaternion.identity, transform.root.Find("Unlock").Find("Lvl3ShoesNew"));
                    rafShoes.Add(temp);
                    temp.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void GetShoes(AICleaner ai = null)
    {
        if (rafShoes.Count <= 0)
            return;
        if (ai == null)
        {
            GameObject temp = rafShoes[rafShoes.Count - 1];
            rafShoes.Remove(temp);
            temp.transform.parent = ballCollector.shoesStackPoint.transform;
            temp.transform.DOLocalMove(new Vector3(0, ballCollector.shoesStackPoint.transform.position.y + (5f * ballCollector.stackingShoesList.Count), 0), 0.25f);
            temp.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ballCollector.stackingShoesList.Add(temp);
            Vibrations.Medium();
        }
        else
        {
            GameObject temp = rafShoes[rafShoes.Count - 1];
            if (temp == null) return;
            rafShoes.Remove(temp);
            temp.transform.parent = ai.shoesStackPoint.transform;
            temp.transform.DOLocalMove(new Vector3(0, ai.shoesStackPoint.transform.position.y + (8f * ai.stackingShoesList.Count), 0), 0.25f);
            temp.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ai.stackingShoesList.Add(temp);
            Vibrations.Medium();
        }
    }
}