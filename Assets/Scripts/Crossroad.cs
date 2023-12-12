using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Crossroad : MonoBehaviour
{
    [SerializeField] protected List<WaypointEdge> waypoints = new List<WaypointEdge>();

    public Transform GetNextWaypoint(WaypointEdge currentWaypoint)
    {      
        return currentWaypoint.GetNext().transform;
    }

    public WaypointEdge getRandomEdge()
    {
        return waypoints[Random.Range(0, waypoints.Count)];
    }
    
}