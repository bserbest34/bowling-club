using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBaseManager : Singleton<UIBaseManager>
{
    Button tapToStartBtn;
    Button tapToRetryBtn;
    Button tapToContinue;
    GameObject successPanel;
    GameObject failPanel;
    TextMeshProUGUI levelText;

    public bool isCpiVideo = false;

    internal delegate void GameStatus();

    internal virtual void Start()
    {
        CPIVideo();
        InitObjects();
        AddListeners();

        //tapToStartBtn.gameObject.SetActive(true);
    }

    internal virtual void TapToContinue()
    {
        tapToContinue.onClick.RemoveAllListeners();
    }

    internal virtual void TapToRetry()
    {
        tapToRetryBtn.onClick.RemoveAllListeners();
    }

    internal virtual void TapToStart()
    {
        tapToStartBtn.onClick.RemoveAllListeners();
        tapToStartBtn.gameObject.SetActive(false);
    }

    internal virtual void SuccesGame()
    {
        if (successPanel.activeSelf) return;
        Vibrations.Succes();
        successPanel.SetActive(true);
    }

    internal virtual void FailGame()
    {
        if (failPanel.activeSelf) return;
        Vibrations.Failure();
        failPanel.SetActive(true);
    }

    void InitObjects()
    {
        levelText = transform.Find("LevelBar").GetComponentInChildren<TextMeshProUGUI>();
        tapToStartBtn = transform.Find("FullscreenButton").GetComponent<Button>();
        failPanel = transform.Find("FullscreenFail").gameObject;
        tapToRetryBtn = failPanel.GetComponentInChildren<Button>();
        successPanel = transform.Find("FullscreenSuccess").gameObject;
        tapToContinue = successPanel.GetComponentInChildren<Button>();
        levelText.SetText("LVL " + PlayerPrefs.GetInt(Key.Level, 1).ToString());
    }

    void AddListeners()
    {
        tapToContinue.onClick.AddListener(TapToContinue);
        tapToRetryBtn.onClick.AddListener(TapToRetry);
        tapToStartBtn.onClick.AddListener(TapToStart);
    }

    public void CPIVideo()
    {
        //if (isCpiVideo)
        //{
        //    foreach (Transform child in transform)
        //    {
        //        if (child.gameObject.GetComponent<CanvasGroup>() == null) continue;
        //        child.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        //    }
        //}
    }
}