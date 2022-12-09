using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallCollectorManager : MonoBehaviour
{
    public List<GameObject> balls = new List<GameObject>();
    internal float posZ;
    bool isInProcess = false;

    private void Update()
    {
        if(balls.Count <= 0 && !isInProcess)
        {
            StartCoroutine(SetAnim());
        }
    }

    IEnumerator SetAnim()
    {
        isInProcess = true;
        transform.parent.Find("BallCollectorArea").transform.DOScale(new Vector3(2.277783f, 0.08276978f, 2.689425f), 1f);
        yield return new WaitForSeconds(1f);
        transform.parent.Find("BallCollectorArea").transform.DOScale(new Vector3(1.775357f, 0.06451269f, 2.0962f), 1f);
        yield return new WaitForSeconds(1f);
        isInProcess = false;
    }
}