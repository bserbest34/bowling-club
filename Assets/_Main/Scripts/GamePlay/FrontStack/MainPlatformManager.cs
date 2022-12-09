using System.Collections.Generic;
using UnityEngine;

public class MainPlatformManager : MonoBehaviour
{
    [SerializeField] public List<Transform> platformList = new List<Transform>();

    private void Awake()
    {
        platformList.Add(transform);
    }
}