using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCollectionManager : MonoBehaviour
{
    public Button openMyCollectionBtn;
    public Button closeMyCollectionBtn;
    internal int lastOpenCollectionIndex = 1;
    public List<Image> collectionBallsImage = new List<Image>();

    private void Awake()
    {
        if (PlayerPrefs.GetInt("OnboardingisAllTutorialDone", 0) == 0)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    void Start()
    {
        lastOpenCollectionIndex = PlayerPrefs.GetInt("lastOpenCollectionIndex", 1);
        openMyCollectionBtn.onClick.AddListener(OpenMyCollection);
        closeMyCollectionBtn.onClick.AddListener(CloseMyCollection);
        SetCollectionUI();
    }

    void OpenMyCollection()
    {
        Vibrations.Selection();
        transform.SetAsLastSibling();
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void CloseMyCollection()
    {
        Vibrations.Selection();
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    internal void SetCollectionUI()
    {
        for (int i = 0; i < lastOpenCollectionIndex; i++)
        {
            collectionBallsImage[i].gameObject.SetActive(true);
        }
    }

    internal void IncreaseValue()
    {
        lastOpenCollectionIndex++;
        PlayerPrefs.SetInt("lastOpenCollectionIndex", lastOpenCollectionIndex);
        SetCollectionUI();
    }
}