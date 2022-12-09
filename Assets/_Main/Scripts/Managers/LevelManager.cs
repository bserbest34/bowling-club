using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class LevelManager : MonoBehaviour
{
    private int level;

    private void Awake()
    {
        GameAnalytics.Initialize();
        DontDestroyOnLoad(gameObject);
        UIManager.OnLoadNextLevel += LoadNextLevel;
        UIManager.OnRetry += LoadSameLevel;
        //StartCoroutine(LoadLevel());
    }

    internal IEnumerator LoadLevel()
    {
        if (PlayerPrefs.GetInt(Key.Level) <= (SceneManager.sceneCountInBuildSettings - 1) && PlayerPrefs.GetInt(Key.Level) > 1)
        {
            level = PlayerPrefs.GetInt(Key.Level);
        }
        else if (PlayerPrefs.GetInt(Key.Level) > (SceneManager.sceneCountInBuildSettings - 1))
        {
            level = PlayerPrefs.GetInt(Key.Level) % (SceneManager.sceneCountInBuildSettings - 1);
            if (level == 0 && PlayerPrefs.GetInt(Key.Level) == 0)
            {
                level = 1;
            }
            else if (level == 0)
            {
                level = (SceneManager.sceneCountInBuildSettings - 1);
            }
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt(Key.Level, 1);
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
    }

    void LoadNextLevel()
    {
        PlayerPrefs.SetInt(Key.Level, PlayerPrefs.GetInt(Key.Level) + 1);
        StartCoroutine(LoadLevel());
    }

    void LoadSameLevel()
    {
        StartCoroutine(LoadLevel());
    }
}