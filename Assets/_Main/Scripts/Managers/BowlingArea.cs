using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingArea : MonoBehaviour
{
    internal int bowlingAreaLevel = 0;
    internal int currentCustomerCount = 0;
    internal int maxCount = 6;
    internal bool isOnPlayOne = false;
    internal int playCountPerCustomer = 2;
    public List<Transform> waitingAreas = new List<Transform>();

    private void Start()
    {
        bowlingAreaLevel = PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + name);
        SetBowlingAreaLevel();   
    }

    internal void SetBowlingAreaLevel()
    {
        bowlingAreaLevel = PlayerPrefs.GetInt(Key.ButtonBowlingUpgrade + name);
        switch (bowlingAreaLevel)
        {
            case 0:
                break;
            case 1:
                maxCount = 1;
                playCountPerCustomer = 2;
                waitingAreas[0].gameObject.SetActive(true);
                break;
            case 2:
                maxCount = 2;
                playCountPerCustomer = 2;
                transform.Find("BowlingArea").gameObject.SetActive(false);
                transform.Find("BowlingArea2").gameObject.SetActive(true);
                waitingAreas[0].gameObject.SetActive(true);
                waitingAreas[1].gameObject.SetActive(true);
                break;
            case 3:
                maxCount = 4;
                playCountPerCustomer = 1;
                transform.Find("BowlingArea").gameObject.SetActive(false);
                transform.Find("BowlingArea2").gameObject.SetActive(false);
                transform.Find("BowlingArea3").gameObject.SetActive(true);
                waitingAreas[0].gameObject.SetActive(true);
                waitingAreas[1].gameObject.SetActive(true);
                waitingAreas[2].gameObject.SetActive(true);
                waitingAreas[3].gameObject.SetActive(true);
                break;
            case 4:
                maxCount = 6;
                playCountPerCustomer = 1;
                transform.Find("BowlingArea").gameObject.SetActive(false);
                transform.Find("BowlingArea2").gameObject.SetActive(false);
                transform.Find("BowlingArea3").gameObject.SetActive(false);
                transform.Find("BowlingArea4").gameObject.SetActive(true);
                transform.Find("UpgradeCanvas").gameObject.SetActive(false);
                transform.Find("BowlingCanvas").gameObject.SetActive(false);
                waitingAreas[0].gameObject.SetActive(true);
                waitingAreas[1].gameObject.SetActive(true);
                waitingAreas[2].gameObject.SetActive(true);
                waitingAreas[3].gameObject.SetActive(true);
                waitingAreas[4].gameObject.SetActive(true);
                waitingAreas[5].gameObject.SetActive(true);
                break;
        }
    }
}