using UnityEngine;

public class TransformDoorManager : MonoBehaviour
{
    [SerializeField] private enum TransformType { Clover, Heart, Hat}
    [SerializeField] private TransformType type;

    [SerializeField] private GameObject[] models;

    private void OnValidate()
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name.Contains(type.ToString()))
                models[i].SetActive(true);
            else
                models[i].SetActive(false);
        }
    }
}