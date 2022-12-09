using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShoes : MonoBehaviour
{
    public List<GameObject> shoes = new List<GameObject>();

    internal void GiveShoesToCustomer()
    {
        if(shoes.Count > 0)
        {
            shoes[shoes.Count - 1].gameObject.SetActive(false);
            shoes.Remove(shoes[shoes.Count - 1]);
        }
    }
}
