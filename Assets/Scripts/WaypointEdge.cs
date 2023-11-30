using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointEdge : MonoBehaviour
{
    [SerializeField] private WaypointEdge[] directions;
    [SerializeField] private bool isMainRoad;
    private int hasCars;
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
        if (!other.gameObject.CompareTag("Car"))
        {
            return;
        }
        hasCars++;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Car"))
        {
            return;
        }
        if (isMainRoad)
        {
            // If the waypoint is on a main road, or the car is not NPC, let it pass
            return;
        }
        // Yield to cars on main roads, if any

        if (transform.parent.GetComponent<Crossroad>().MainRoadsEmpty())
        {
            // Main road empty, deactivate the barrier
            var barrier = transform.GetChild(0).gameObject;
            if (barrier.activeSelf)
            {
                barrier.SetActive(false);
                other.gameObject.GetComponent<CarMover>().setBrake(false);
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Car"))
        {
            return;
        }
        hasCars--;
    }

    public WaypointEdge GetNext()
    {
        if (directions.Length > 0)
        {
            int randomNumber = Random.Range(0, directions.Length);
            
            return directions[randomNumber];
        }
        else
        {
            return null;
        }
    }

    public bool IsMainRoad()
    {
        return isMainRoad;
    }

    public bool HasCars()
    {
        return hasCars != 0;
    }
}
