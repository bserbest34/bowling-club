using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Data;
using System.Linq;
using System.Collections;

#if UNITY_EDITOR

public class PlayerPrefsEditor : EditorWindow
{
    public Dictionary<string, object> dict;
    float timer = 0;
    bool isRefreshProcess = false;
    bool isSaveProcess = false;
    bool isDeleteProcess = false;

    [MenuItem("DliteGames/PlayerPrefs Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsEditor>("PlayerPrefs Editor");
    }

    private void OnInspectorUpdate()
    {
        if (Time.realtimeSinceStartup - timer > 4f && (isRefreshProcess || isSaveProcess || isDeleteProcess))
        {
            GetPlayerPrefsValues();
            isRefreshProcess = false;
            isSaveProcess = false;
            isDeleteProcess = false;
            Repaint();
        }
    }

    GUILayoutOption[] labelStyle = new GUILayoutOption[] { GUILayout.Width((float)100), GUILayout.MaxHeight((float)15) };
    GUILayoutOption[] buttonStyle = new GUILayoutOption[] { GUILayout.Height((float)50), GUILayout.MaxWidth((float)1000) };

    private void OnGUI()
    {
        if (!isRefreshProcess && !isSaveProcess && !isDeleteProcess)
        {
            EditorGUILayout.HelpBox("DliteGames PlayerPrefs Editor", MessageType.None);

            if (dict != null)
            {
                foreach (var item in dict.ToList().OrderBy(x => x.Key))
                {
                    if (item.Key.StartsWith("unity") || item.Key.StartsWith("Unity"))
                        continue;
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(item.Key);

                    switch (item.Key)
                    {
                        case Key.Money:
                            dict[item.Key] = EditorGUILayout.FloatField(Convert.ToSingle(item.Value), labelStyle);
                            break;
                        case Key.Level:
                            dict[item.Key] = EditorGUILayout.IntField(Convert.ToInt32(item.Value), labelStyle);
                            break;
                        default:
                            GUILayout.Label(item.Value.ToString(), labelStyle);
                            break;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Refresh", buttonStyle))
            {
                isRefreshProcess = true;
                timer = Time.realtimeSinceStartup;
            }

            if (dict == null)
                return;
            if (GUILayout.Button("Clear", buttonStyle))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                isDeleteProcess = true;
                timer = Time.realtimeSinceStartup;
            }

            if (GUILayout.Button("Save", buttonStyle))
            {
                foreach (var item in dict.ToList())
                {
                    if (item.Key.StartsWith("unity") || item.Key.StartsWith("Unity"))
                        continue;

                    switch (item.Key)
                    {
                        case Key.Money:
                            PlayerPrefs.SetFloat(item.Key, Convert.ToSingle(item.Value));
                            break;
                        case Key.Level:
                            PlayerPrefs.SetInt(item.Key, Convert.ToInt32(item.Value));
                            break;
                        default:

                            break;
                    }
                }
                PlayerPrefs.Save();
                isSaveProcess = true;
                timer = Time.realtimeSinceStartup;
            }

            EditorGUILayout.EndHorizontal();
        }
        else if (isRefreshProcess)
        {
            EditorGUILayout.HelpBox("Degisiklikler Getiriliyor..", MessageType.Info);
        }
        else if(isSaveProcess)
        {
            EditorGUILayout.HelpBox("Degisiklikler Kaydediliyor..", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("Playerprefs Sifirlaniyor..", MessageType.Info);
        }
    }

    private void Type(object value)
    {
        throw new NotImplementedException();
    }

    void GetPlayerPrefsValues()
    {
#if UNITY_EDITOR_OSX
        dict = (Dictionary<string, object>)Plist.readPlist(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
            "/Library/Preferences/unity." + Application.companyName + "." + Application.productName + ".plist");
#else
        dict = (Dictionary<string, object>)Plist.readPlist("HKCU'\'Software'\'" + Application.companyName + "'\'" + Application.productName);
#endif
        Repaint();
    }

}

#endif