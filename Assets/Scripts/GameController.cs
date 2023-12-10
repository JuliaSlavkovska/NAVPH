using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> crossroads = new List<GameObject>();
    [SerializeField] private List<GameObject> carPrefabs = new List<GameObject>();
    [SerializeField] GameObject allCars;

    void Start()
    {


        for(var i = 0; i < 100; ++i)
        {
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        // Get random waypoint from a random crossroad
        var idx = Random.Range(0, crossroads.Count);
        var waypoint = crossroads[idx].GetComponent<Crossroad>().getRandomEdge();
        if (!waypoint)
            Debug.Log(idx);


        GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)]);
        newCar.GetComponent<CarMover>().setWaypoint(waypoint);
        newCar.transform.parent = allCars.transform;

        newCar.transform.LookAt(waypoint.transform);
        newCar.transform.position = waypoint.transform.position;

        
    }

}
