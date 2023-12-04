using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointEdge : MonoBehaviour
{
    [SerializeField] private WaypointEdge[] directions;
    [SerializeField] private bool isMainRoad;
    [SerializeField] public bool isEntrance;
    // The exit node in the left direction (for yielding for opposite cars when turning left)
    [SerializeField] public WaypointEdge leftTurn;
    // The entrance opposite node
    [SerializeField] public WaypointEdge opposite;
    // The node to the left, which will be blocked on unmarked crossroads (yielding right)
    [SerializeField] public WaypointEdge leftYield;
    [SerializeField] public int numOfCars = 0;
    [SerializeField] public GameObject blocker;
    
    [ColorUsage(false, true)] public Color color = Color.blue;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, 1f);

        Gizmos.color = Color.green;

        if (directions != null)
        {
            for (int i = 0; i <= directions.Length - 1; i++)
            {
                Gizmos.DrawLine(transform.position, directions[i].transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMainRoad && other.gameObject.CompareTag("Car"))
        {
            numOfCars++;
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (isMainRoad && other.gameObject.CompareTag("Car"))
        {
            numOfCars--;
        }

    }

    public WaypointEdge GetNext()
    {
        if (directions.Length > 0)
        {
            int randomNumber = Random.Range(0, directions.Length);
            
            return directions[randomNumber];
        }

        return null;
    }

}
