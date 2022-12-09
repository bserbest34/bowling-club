using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PinMaker : MonoBehaviour
{
    internal bool isTrigger = false;
    internal bool isTrigger2 = false;
    Animator anim;
    Transform pins;

    private void Start()
    {
        if(transform.root.Find("Pins") != null)
        {
            pins = transform.root.Find("Pins");
        }
        else
        {
            pins = transform.root.Find("BowlingArea").Find("Pins");
        }
        anim = transform.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isTrigger)
        {
            anim.SetBool("isTrigger", true);
        }
        else
        {
            anim.SetBool("isTrigger", false);
        }

        if (isTrigger2)
        {
            anim.SetBool("isTrigger2", true);
        }
        else
        {
            anim.SetBool("isTrigger2", false);
        }
    }

    #region Bowling 1. Durum
    internal void PinsSetTrue()
    {
        if(pins.GetComponent<Animator>().GetInteger("PinIndex") == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < 4; i++)
            {
                pins.GetComponent<Pins>().labutlar[i].transform.localScale = new Vector3(0, 0, 0);
            }
            isTrigger = false;
            pins.GetComponent<Animator>().SetBool("isDone", true);
        }
    }
    internal void PinsSetFalse()
    {
        if (pins.GetComponent<Animator>().GetInteger("PinIndex") == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            pins.GetComponent<Animator>().SetBool("isDone", false);
        }
    }
    internal IEnumerator FourPinsTrue()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.2f);
            pins.GetComponent<Pins>().labutlar[i].transform.DOScale(672.0782f, 0.15f);
        }
    }

    #endregion


    internal void PinsFullFalse()
    {
        if(pins.GetComponent<Animator>().GetInteger("PinIndex") == 1)
        {
            for (int i = 0; i < pins.GetComponent<Pins>().labutlar.Count; i++)
            {
                pins.GetComponent<Pins>().labutlar[i].transform.localScale = new Vector3(0, 0, 0);
            }
        }
        isTrigger2 = false;
        pins.GetComponent<Animator>().SetBool("isDone", true);
    }
    internal IEnumerator FullPinsTrue()
    {
        for (int i = 0; i < pins.GetComponent<Pins>().labutlar.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            pins.GetComponent<Pins>().labutlar[i].transform.DOScale(672.0782f, 0.15f);
        }
        pins.GetComponent<Animator>().SetBool("isDone", false);
    }
}