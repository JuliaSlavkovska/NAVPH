using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//script for initial car spawning
public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> carPrefabs;
    [SerializeField] private List<WaypointEdge> spawnWaypoints;
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
