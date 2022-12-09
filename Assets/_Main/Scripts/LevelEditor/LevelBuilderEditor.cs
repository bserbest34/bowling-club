using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(LevelBuilderScript))]
public class LevelBuilderEditor : Editor
{
    public int roadCount = 0;
    public int obstacleCount = 0;
    public int obstacleCount2 = 0;
    public int benefitObjectCount = 0;
    public int benefitObjectCount2 = 0;
    public int doorCount = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelBuilderScript myScript = (LevelBuilderScript)target;

        GUILayoutOption[] sliderStyle = new GUILayoutOption[] { GUILayout.MaxWidth((float)1000), GUILayout.MaxHeight((float)30) };
        GUILayoutOption[] labelStyle = new GUILayoutOption[] { GUILayout.MaxWidth((float)1000), GUILayout.MaxHeight((float)15) };

        GUILayout.Space(50f);

        GUIContent doorContent = new GUIContent("Door Object Count" + " = " + doorCount.ToString());
        GUILayout.Label(doorContent, labelStyle);
        doorCount = (int)GUILayout.HorizontalSlider((float)doorCount, 0, 100, sliderStyle);

        GUIContent roadContent = new GUIContent("Road Object Count" + " = " + roadCount.ToString());
        GUILayout.Label(roadContent, labelStyle);
        roadCount = (int)GUILayout.HorizontalSlider((float)roadCount, 0, 50, sliderStyle);

        GUIContent labelContent = new GUIContent("Obstacle Count" + " = " + obstacleCount.ToString());
        GUILayout.Label(labelContent, labelStyle);
        obstacleCount = (int)GUILayout.HorizontalSlider((float)obstacleCount, 0, 30, sliderStyle);

        GUIContent labelContent2 = new GUIContent("Obstacle Count2" + " = " + obstacleCount2.ToString());
        GUILayout.Label(labelContent2, labelStyle);
        obstacleCount2 = (int)GUILayout.HorizontalSlider((float)obstacleCount2, 0, 30, sliderStyle);

        GUIContent benefitContent = new GUIContent("Benefit Object Count" + " = " + benefitObjectCount.ToString());
        GUILayout.Label(benefitContent, labelStyle);
        benefitObjectCount = (int)GUILayout.HorizontalSlider((float)benefitObjectCount, 0, 100, sliderStyle);

        GUIContent benefitContent2 = new GUIContent("Benefit Object Count2" + " = " + benefitObjectCount2.ToString());
        GUILayout.Label(benefitContent2, labelStyle);
        benefitObjectCount2 = (int)GUILayout.HorizontalSlider((float)benefitObjectCount2, 0, 100, sliderStyle);

        if (GUILayout.Button("Build Level"))
        {
            Debug.Log("Level Created");
            myScript.BuildObject(roadCount, obstacleCount, benefitObjectCount, doorCount, obstacleCount2, benefitObjectCount2);
        }
    }

}

#endif