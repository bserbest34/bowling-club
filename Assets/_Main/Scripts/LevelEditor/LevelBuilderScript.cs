using UnityEngine;

#if UNITY_EDITOR

public class LevelBuilderScript : MonoBehaviour
{
    [Header("Door Object")]
    public GameObject emptyDoorsObject;
    public GameObject doorPrefab;
    GameObject doors;
    public float doorYPoint = 0;
    public Vector3 doorRotation;
    public float beginDoorPointInZ;
    public float distanceBetweenDoors;

    [Header("Obstacle 1")]
    public GameObject emptyObstaclesObject;
    public GameObject obstaclePrefab;
    GameObject obstacles;
    public float obstacleYPoint = 0;
    public Vector3 obstacleRotation;
    public float beginObstaclePointInZ;
    public float distanceBetweenObstacles;

    [Header("Obstacle 2")]
    public GameObject emptyObstaclesObject2;
    public GameObject obstaclePrefab2;
    GameObject obstacles2;
    public float obstacleYPoint2 = 0;
    public Vector3 obstacleRotation2;
    public float beginObstaclePointInZ2;
    public float distanceBetweenObstacles2;

    [Header("Benefit Object 1")]
    public GameObject emptyBenefitObject;
    public GameObject benefitObjectPrefab;
    GameObject benefits;
    public float benefitYPoint = 0;
    public Vector3 benefitRotation;
    public float beginBenefitPointInZ;
    public float distanceBetweenBenefits;

    [Header("Benefit Object 2")]
    public GameObject emptyBenefitObject2;
    public GameObject benefitObjectPrefab2;
    GameObject benefits2;
    public float benefitYPoint2 = 0;
    public Vector3 benefitRotation2;
    public float beginBenefitPointInZ2;
    public float distanceBetweenBenefits2;

    [Header("Road Object")]
    public GameObject emptyRoadObject;
    public GameObject roadPrefab;
    public GameObject finishLine;
    GameObject roads;
    public float roadYPoint = 0;
    public Vector3 roadRotation;

    public void BuildObject(int roadCount, int obstacleCount, int benefitObjectCount, int doorCount, int obstacleCount2, int benefitObjectCount2)
    {
        CreateRoads(roadCount);
        CreateObstacles(obstacleCount);
        CreateObstacles2(obstacleCount2);
        CreateBenefitObjects(benefitObjectCount);
        CreateBenefitObjects2(benefitObjectCount2);
        CreateDoorObjects(doorCount);
    }

    void CreateRoads(int roadCount)
    {
        if (GameObject.Find("LevelEditor_Roads(Clone)") != null)
        {
            DestroyImmediate(roads);
            Instantiate(emptyRoadObject, Vector3.zero, Quaternion.identity, null);
            roads = GameObject.Find("LevelEditor_Roads(Clone)");
        }
        else
        {
            Instantiate(emptyRoadObject, Vector3.zero, Quaternion.identity, null);
            roads = GameObject.Find("LevelEditor_Roads(Clone)");
        }

        for (int roadIndex = 0; roadIndex < roadCount; roadIndex++)
        {
            Instantiate(roadPrefab, new Vector3(0, 0, roadIndex * 33.5682f), Quaternion.identity, roads.transform);
            if (roadIndex == roadCount - 1)
            {
                Instantiate(finishLine, new Vector3(0, 0.68f, (roadIndex * 33.5682f) - 3f),
                    Quaternion.Euler(roadRotation), roads.transform);
            }
        }
    }

    void CreateObstacles(int obstacleCount)
    {
        if (GameObject.Find("LevelEditor_Obstacles(Clone)") != null)
        {
            DestroyImmediate(obstacles);
            Instantiate(emptyObstaclesObject, Vector3.zero, Quaternion.identity, null);
            obstacles = GameObject.Find("LevelEditor_Obstacles(Clone)");
        }
        else
        {
            Instantiate(emptyObstaclesObject, Vector3.zero, Quaternion.identity, null);
            obstacles = GameObject.Find("LevelEditor_Obstacles(Clone)");
        }

        for (int obstacleIndex = 0; obstacleIndex < obstacleCount; obstacleIndex++)
        {
            Instantiate(obstaclePrefab, new Vector3(Random.Range(-FindObjectOfType<SwerveInput>().clampingBoundaryInX,
                FindObjectOfType<SwerveInput>().clampingBoundaryInX), obstacleYPoint, (obstacleIndex * distanceBetweenObstacles) + beginObstaclePointInZ),
                Quaternion.Euler(obstacleRotation), obstacles.transform);
        }
    }

    void CreateBenefitObjects(int benefitObjectCount)
    {
        if (GameObject.Find("LevelEditor_Benefits(Clone)") != null)
        {
            DestroyImmediate(benefits);
            Instantiate(emptyBenefitObject, Vector3.zero, Quaternion.identity, null);
            benefits = GameObject.Find("LevelEditor_Benefits(Clone)");
        }
        else
        {
            Instantiate(emptyBenefitObject, Vector3.zero, Quaternion.identity, null);
            benefits = GameObject.Find("LevelEditor_Benefits(Clone)");
        }

        for (int benefitObjectIndex = 0; benefitObjectIndex < benefitObjectCount; benefitObjectIndex++)
        {
            Instantiate(benefitObjectPrefab, new Vector3(Random.Range(-FindObjectOfType<SwerveInput>().clampingBoundaryInX,
                FindObjectOfType<SwerveInput>().clampingBoundaryInX), benefitYPoint, (benefitObjectIndex * distanceBetweenBenefits) + beginBenefitPointInZ),
                Quaternion.Euler(benefitRotation), benefits.transform);
        }
    }

    void CreateObstacles2(int obstacleCount2)
    {
        if (GameObject.Find("LevelEditor_Obstacles2(Clone)") != null)
        {
            DestroyImmediate(obstacles2);
            Instantiate(emptyObstaclesObject2, Vector3.zero, Quaternion.identity, null);
            obstacles2 = GameObject.Find("LevelEditor_Obstacles2(Clone)");
        }
        else
        {
            Instantiate(emptyObstaclesObject2, Vector3.zero, Quaternion.identity, null);
            obstacles2 = GameObject.Find("LevelEditor_Obstacles2(Clone)");
        }

        for (int obstacleIndex = 0; obstacleIndex < obstacleCount2; obstacleIndex++)
        {
            Instantiate(obstaclePrefab2, new Vector3(Random.Range(-FindObjectOfType<SwerveInput>().clampingBoundaryInX,
                FindObjectOfType<SwerveInput>().clampingBoundaryInX), obstacleYPoint2, (obstacleIndex * distanceBetweenObstacles2) + beginObstaclePointInZ2),
                Quaternion.Euler(obstacleRotation2), obstacles2.transform);
        }
    }

    void CreateBenefitObjects2(int benefitObjectCount2)
    {
        if (GameObject.Find("LevelEditor_Benefits2(Clone)") != null)
        {
            DestroyImmediate(benefits2);
            Instantiate(emptyBenefitObject2, Vector3.zero, Quaternion.identity, null);
            benefits2 = GameObject.Find("LevelEditor_Benefits2(Clone)");
        }
        else
        {
            Instantiate(emptyBenefitObject2, Vector3.zero, Quaternion.identity, null);
            benefits2 = GameObject.Find("LevelEditor_Benefits2(Clone)");
        }

        for (int benefitObjectIndex = 0; benefitObjectIndex < benefitObjectCount2; benefitObjectIndex++)
        {
            Instantiate(benefitObjectPrefab2, new Vector3(Random.Range(-FindObjectOfType<SwerveInput>().clampingBoundaryInX,
                FindObjectOfType<SwerveInput>().clampingBoundaryInX), benefitYPoint2, (benefitObjectIndex * distanceBetweenBenefits2) + beginBenefitPointInZ2),
                Quaternion.Euler(benefitRotation2), benefits2.transform);
        }
    }

    void CreateDoorObjects(int doorObjectCount)
    {
        if (GameObject.Find("LevelEditor_Doors(Clone)") != null)
        {
            DestroyImmediate(doors);
            Instantiate(emptyDoorsObject, Vector3.zero, Quaternion.identity, null);
            doors = GameObject.Find("LevelEditor_Doors(Clone)");
        }
        else
        {
            Instantiate(emptyDoorsObject, Vector3.zero, Quaternion.identity, null);
            doors = GameObject.Find("LevelEditor_Doors(Clone)");
        }

        for (int doorObjectIndex = 0; doorObjectIndex < doorObjectCount; doorObjectIndex++)
        {
            Instantiate(doorPrefab, new Vector3(Random.Range(-FindObjectOfType<SwerveInput>().clampingBoundaryInX,
                FindObjectOfType<SwerveInput>().clampingBoundaryInX), doorYPoint, (doorObjectIndex * distanceBetweenDoors) + beginDoorPointInZ),
                Quaternion.Euler(doorRotation), doors.transform);
        }
    }
}

#endif