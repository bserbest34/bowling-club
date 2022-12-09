using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetFollow : MonoBehaviour
{
    NavMeshAgent agent;
    BallCollector ballCollector;
    float startTime = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ballCollector = FindObjectOfType<BallCollector>();
    }

    private void OnEnable()
    {
        startTime = Time.time;
        if(transform.name == "RobotGround")
        {
            FindObjectOfType<JoystickControl>().movSpeed += 4;
        }else
        {
            FindObjectOfType<MoneyManager>().isPetEnabled = 2;
        }
    }

    void Update()
    {
        if (Time.time - startTime > 45f)
        {
            if (transform.name == "RobotGround")
            {
                FindObjectOfType<JoystickControl>().movSpeed -= 4;
            }
            else
            {
                FindObjectOfType<MoneyManager>().isPetEnabled = 1;
            }
            Destroy(gameObject);
            return;
        }

        agent.SetDestination(ballCollector.transform.position);
        agent.speed = (Vector3.Distance(transform.position, ballCollector.transform.position) * 2);
    }
}