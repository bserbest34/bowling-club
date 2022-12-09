using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerManager : MonoBehaviour
{
    public List<GameObject> waiters = new List<GameObject>();
    public List<GameObject> customers = new List<GameObject>();
    private Transform[] lines;

    public GameObject AIPrefab;
    public GameObject AIWomenPrefab;
    private Transform spawnPoint;
    private Transform lookAt;
    
    private void Start()
    {
        lines = GameObject.Find("Lines").GetComponentsInChildren<Transform>();
        spawnPoint = GameObject.Find("AISpawnPoint").transform;
        lookAt = GameObject.Find("Target").transform;

        foreach (var item in GameObject.FindGameObjectsWithTag("Waiter"))
        {
            waiters.Add(item);
        }
        AISirayaGirme();
    }

    internal void AISirayaGirme()
    {
        for (int i = 0; i < waiters.Count; i++)
        {
            if (i == 0)
                continue;
            if(lines.Length > i)
            {
                waiters[i - 1].GetComponent<NavMeshAgent>().SetDestination(lines[i].position);
                if (i == 0)
                {
                    waiters[1].transform.LookAt(lookAt);
                }
                else
                {
                    waiters[i].transform.LookAt(waiters[i - 1].transform);
                }
            }
        }
    }

    internal void InstatiateAIs()
    {
        int random = Random.Range(1, 3);
        if(random == 1)
        {
            GameObject ai;
            ai = Instantiate(AIPrefab, spawnPoint.transform.position, Quaternion.identity);
            waiters.Add(ai);
            ai.transform.Find("Character").GetChild(Random.Range(4, 8)).gameObject.SetActive(true);
        }
        else if(random == 2)
        {
            GameObject aiWomen;
            aiWomen = Instantiate(AIWomenPrefab, spawnPoint.transform.position, Quaternion.identity);
            waiters.Add(aiWomen);
            aiWomen.transform.GetChild(Random.Range(7, 10)).gameObject.SetActive(true);
        }
    }
}