using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : Singleton<LevelEditor>
{
    public GameObject levelEditorManager;
    static GameObject _levelEditorManager;

    void OnValidate()
    {
        _levelEditorManager = levelEditorManager;
    }

    internal static void CreateLevelEditorManager()
    {
        if (GameObject.Find("LevelEditorManager(Clone)"))
        {
            Debug.LogWarning("You already have an Level Editor Manager --DliteGames--");
        }
        else
        {
            Instantiate(_levelEditorManager);
        }
    }
}
