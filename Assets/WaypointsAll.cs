using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WaypointsAll : MonoBehaviour
{
    [SerializeField] private WaypointEdge[] waipointsEdges;
    // Start is called before the first frame update

    public Transform GetNextWaypoint(WaypointEdge currentWaypoint)
    {      
        return currentWaypoint.GetNext().transform;
    }

    public WaypointEdge getFirstEdge()
    {
        return waipointsEdges[7];
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
