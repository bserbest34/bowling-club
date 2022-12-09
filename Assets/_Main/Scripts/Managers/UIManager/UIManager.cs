using UnityEngine;
using UnityEngine.UI;

public class UIManager : UIBaseManager
{
    internal static event GameStatus OnStart;
    internal static event GameStatus OnSuccess;
    internal static event GameStatus OnFail;
    internal static event GameStatus OnRetry;
    internal static event GameStatus OnLoadNextLevel;
    public CanvasGroup canvasMoney;
    public CanvasGroup canvasDaily;

    private void Update()
    {
        if (transform.Find("Panel") == null)
            return;
        if(transform.Find("Panel").Find("LoadingBar").Find("FinishBarr").GetComponent<Image>().fillAmount >= 0.98f)
        {
            Destroy(transform.Find("Panel").gameObject);
            canvasMoney.alpha = 1;
            canvasDaily.alpha = 1;
        }
        else
        {
            transform.Find("Panel").Find("LoadingBar").Find("FinishBarr").GetComponent<Image>().fillAmount += 0.3f * Time.deltaTime;
        }
    }

    internal override void Start()
    {
        base.Start();
        OnStart?.Invoke();
    }

    internal override void SuccesGame()
    {
        base.SuccesGame();
        OnSuccess?.Invoke();
    }

    internal override void FailGame()
    {
        base.FailGame();
        OnFail?.Invoke();
    }

    internal override void TapToStart()
    {
        base.TapToStart();
        //OnStart?.Invoke();
    }

    internal override void TapToContinue()
    {
        base.TapToContinue();
        OnLoadNextLevel?.Invoke();
    }

    internal override void TapToRetry()
    {
        base.TapToRetry();
        OnRetry?.Invoke();
    }
}