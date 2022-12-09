using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaitingAreaType
{
    sit, stand
}

public class WaitingArea : MonoBehaviour
{
    public WaitingAreaType type = WaitingAreaType.sit;
    public bool isFill = false;
}