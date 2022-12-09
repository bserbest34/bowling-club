using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyInstantiateSystem : MonoBehaviour
{
    GameObject pathMoney;
    GameObject moneySpawnPoint;

    private void Start()
    {
        pathMoney = transform.Find("PathMoney").gameObject;
        moneySpawnPoint = transform.Find("PathInstantiatePoint").gameObject;
        StartCoroutine(SpawnMoney());
    }

    IEnumerator SpawnMoney()
    {
        for (int i = 0; i < 8; i++)
        {
            var temp = Instantiate(pathMoney, moneySpawnPoint.transform.position, moneySpawnPoint.transform.rotation, transform);
            temp.SetActive(true);
            Destroy(temp, 0.65f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
