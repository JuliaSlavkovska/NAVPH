using System.Collections.Generic;
using UnityEngine;

//script for initial car spawning
public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> carPrefabs;
    [SerializeField] private List<WaypointEdge> spawnWaypoints;
    [SerializeField] private int carCount;
    [SerializeField] private GameObject allCars;


    private void Start()
    {
        for(var i = 0; i < carCount; ++i)
        {
            SpawnCar(i);
        }
    }

    private void SpawnCar(int i)
    {
        // Get random waypoint from a random crossroad
        WaypointEdge waypoint;
        do
        {
            waypoint = spawnWaypoints[Random.Range(0, spawnWaypoints.Count)];
        } while (waypoint.spawnedCar);

        waypoint.spawnedCar = true;

        var newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)]);
        newCar.GetComponent<CarMover>().setWaypoint(waypoint);
        newCar.transform.parent = allCars.transform;
        newCar.name = "Car " + i;

        newCar.transform.LookAt(waypoint.transform);
        newCar.transform.position = waypoint.transform.position;
    }
}