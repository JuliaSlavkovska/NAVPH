using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointEdge : MonoBehaviour
{
    [SerializeField] private WaypointEdge[] directions;
    [SerializeField] private bool isMainRoad;
    [SerializeField] private SignsCrossroad crossroad;
    [SerializeField] public bool isEntrance;
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
            crossroad.carsOnMain++;
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (isMainRoad && other.gameObject.CompareTag("Car"))
        {
            crossroad.carsOnMain--;
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
