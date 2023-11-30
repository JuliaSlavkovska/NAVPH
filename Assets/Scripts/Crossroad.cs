using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CrossroadType {Unmarked, Signs, Lights }

public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<WaypointEdge> waypointsEdges = new List<WaypointEdge>();

    [SerializeField] private CrossroadType type;
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

    public bool MainRoadsEmpty()
    {
        foreach (var waypointEdge in waypointsEdges)
        {
            if (waypointEdge.IsMainRoad() && waypointEdge.HasCars())
            {
                return false;
            }
        }

        return true;
    }
}
