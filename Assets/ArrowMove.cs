using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowMove : MonoBehaviour
{
    bool isOnProcess = false;
    void Update()
    {
        if (isOnProcess) return;
        StartCoroutine(UpDownMovement());
    }

    IEnumerator UpDownMovement()
    {
        isOnProcess = true;
        transform.DOMoveY(15, 1);
        yield return new WaitForSeconds(1);
        transform.DOMoveY(10, 1);
        isOnProcess = false;
    }
}
