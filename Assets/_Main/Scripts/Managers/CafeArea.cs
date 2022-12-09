using System.Collections.Generic;
using UnityEngine;

public class CafeArea : MonoBehaviour
{
    internal int currentCafeCustomer;
    internal int currentCafeChair1;
    internal int currentCafeChair2;
    internal int currentCafeChair3;

    public List<GameObject> cafeCustomers = new List<GameObject>();
    public List<GameObject> cafeChair1 = new List<GameObject>();
    public List<GameObject> cafeChair2 = new List<GameObject>();
    public List<GameObject> cafeChair3 = new List<GameObject>();
}
