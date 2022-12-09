using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class MenuItems
{
    [MenuItem("DliteGames/Level Editor")]
    private static void NewMenuOption()
    {
        LevelEditor.CreateLevelEditorManager();
    }
}

#endif