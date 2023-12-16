using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointEdge : MonoBehaviour
{
    
    public enum Direction
    {
        Left,
        Straight,
        Right
    }
    
    // IMPORTANT - this projects code relies on WaypointEdge having the directions ordered:
    // 0 = Left, 1 = Straight, 2 = Right
    [SerializeField] private WaypointEdge[] directions;
    [SerializeField] private WaypointEdge leftWaypoint;
    [SerializeField] private WaypointEdge straightWaypoint;
    [SerializeField] private WaypointEdge rightWaypoint;
    public bool spawnedCar = false;
    [SerializeField] private bool isMainRoad;
    [SerializeField] public bool isEntrance;
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

        if (directions.Length != 0)
        {
            for (int i = 0; i <= directions.Length - 1; i++)
            {
                if (directions[i])
                    Gizmos.DrawLine(transform.position, directions[i].transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMainRoad && (other.gameObject.CompareTag("Car") ||
            other.gameObject.CompareTag("Player")))
        {
            numOfCars++;
        }

        if (other.gameObject.CompareTag("Car"))
        {
            // Choose a next waypoint for the car
            var next = GetNext();
            Direction direction = Direction.Straight;
            if (next == leftWaypoint)
                direction = Direction.Left;
            else if (next == rightWaypoint)
                direction = Direction.Right;
            else if (next == straightWaypoint)
                direction = Direction.Straight;
            
            var carMover = other.GetComponent<CarMover>();
            carMover.setNextWaypoint(next, direction);

            if (!isMainRoad || carMover.turningLeft)
                carMover.maxSpeed = carMover.crossroadSpeedConstant;

        }

    }



    private void OnTriggerExit(Collider other)
    {
        if (isMainRoad && (other.gameObject.CompareTag("Car") ||
            other.gameObject.CompareTag("Player")))
        {
            numOfCars--;
            
        }

        if (other.CompareTag("Car"))
        {
            var carMover = other.GetComponent<CarMover>();

            carMover.maxSpeed = carMover.maxSpeedConstant;
        }


    }

    public WaypointEdge GetNext()
    {
        int idx = 0;
        do
        {
            idx = Random.Range(0, directions.Length);
        } while (!directions[idx]);
        return directions[idx];

    }

}
