using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ballCollectorAIs;
    public GameObject robot;
    public Transform robotTransform;
    public Transform ballCollectorTransform;

    public void SpawnBallCollectorAIs()
    {
        StartCoroutine(SpawnBallCollectorAIsLate());
    }

    IEnumerator SpawnBallCollectorAIsLate()
    {
        yield return new WaitForSeconds(60f);
        Instantiate(ballCollectorAIs, ballCollectorTransform.position, Quaternion.identity);
    }

    public void SpawnRobots()
    {
        StartCoroutine(SpawnRobotsLate());
    }

    IEnumerator SpawnRobotsLate()
    {
        yield return new WaitForSeconds(120f);
        Instantiate(robot, robotTransform.position, Quaternion.identity);
    }
}