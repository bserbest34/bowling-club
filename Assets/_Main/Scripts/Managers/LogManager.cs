using HomaGames.HomaBelly;
using UnityEngine;
using GameAnalyticsSDK;

public class LogManager : Singleton<LogManager>
{
    float playTime;
    float minute = 0;

    private void Awake()
    {
        DefaultAnalytics.GameplayStarted();
        UIManager.OnStart += LevelStarted;
        UIManager.OnSuccess += Success;
        UIManager.OnFail += Fail;
        playTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - playTime > 60)
        {
            minute++;
            GameAnalytics.NewDesignEvent("PlaytimeMinutes:" + minute);
            playTime = Time.time;
        }
    }

    void LevelStarted()
    {
        DefaultAnalytics.LevelStarted(PlayerPrefs.GetInt("Level"));
    }

    void Fail()
    {
        DefaultAnalytics.LevelFailed();
    }

    void Success()
    {
        DefaultAnalytics.LevelCompleted();
    }
}