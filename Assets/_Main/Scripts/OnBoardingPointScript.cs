using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardingPointScript : MonoBehaviour
{
    float time = 0;
    OnboardingManager onboardingManager;
    internal bool isCol = false;
    public Transform beforeTransform;

    private void Start()
    {
        onboardingManager = FindObjectOfType<OnboardingManager>();

        if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone") == 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            time = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Time.time - time > 0.5f && (beforeTransform == null || beforeTransform.GetComponent<OnBoardingPointScript>().isCol))
            {
                if(!isCol)
                {
                    isCol = true;
                    onboardingManager.SetShowed(transform);
                }
            }
        }
    }
}