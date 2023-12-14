using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> crossroads = new List<GameObject>();
    [SerializeField] private List<GameObject> carPrefabs = new List<GameObject>();
    [SerializeField] private List<WaypointEdge> spawnWaypoints = new List<WaypointEdge>();
    [SerializeField] private int carCount;
    [SerializeField] GameObject allCars;

    void Start()
    {


        for(var i = 0; i < carCount; ++i)
        {
            SpawnCar(i);
        }
    }

    void SpawnCar(int i)
    {
        // Get random waypoint from a random crossroad
        WaypointEdge waypoint;
        do
        {
            waypoint = spawnWaypoints[Random.Range(0, spawnWaypoints.Count)];
        } while (waypoint.spawnedCar);

        waypoint.spawnedCar = true;
        
        GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)]);
        newCar.GetComponent<CarMover>().setWaypoint(waypoint);
        newCar.transform.parent = allCars.transform;
        newCar.name = "Car " + i;

        newCar.transform.LookAt(waypoint.transform);
        newCar.transform.position = waypoint.transform.position;
            

        
    }

}
