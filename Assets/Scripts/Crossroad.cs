using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<WaypointEdge> waypointsEdges = new List<WaypointEdge>();
    // Start is called before the first frame update

    public Transform GetNextWaypoint(WaypointEdge currentWaypoint)
    {      
        return currentWaypoint.GetNext().transform;
    }

    public WaypointEdge getFirstEdge()
    {
        return waypointsEdges[7];
    }

    public WaypointEdge getRandomEdge()
    {
        return waypointsEdges[Random.Range(0, waypointsEdges.Count)];
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
