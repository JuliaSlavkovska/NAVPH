using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crossroad : MonoBehaviour
{
    [SerializeField] protected List<WaypointEdge> waypointsEdges = new List<WaypointEdge>();

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
    
}