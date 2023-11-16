using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class CarMover : MonoBehaviour
{
    // Uklada referenciu na waypoint system ktory tento objekt pouziva
    private WaypointEdge currentWaypoint;
    private WaypointEdge nextWaypoint;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 0.3f;
    [SerializeField] private float brakeForce = 0.6f;
    [SerializeField] private float rotateSpeed = 4f;
    private Quaternion rotationGoal;
    private Vector3 directionToWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        //nastavi dalsi waypoint ciel
        //nextWaypoint = currentWaypoint.GetNext();
        //transform.position = nextWaypoint.transform.position;
        nextWaypoint = currentWaypoint.GetNext();
        if (nextWaypoint == null)
        {
            nextWaypoint = currentWaypoint;
        }
        else
        {
            currentWaypoint = nextWaypoint;
        }
        transform.LookAt(nextWaypoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Accelerate();   
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, nextWaypoint.transform.position) < 0.1f)
        {
            nextWaypoint = currentWaypoint.GetNext();
            if (nextWaypoint == null) {
                nextWaypoint = currentWaypoint;
            }
            else {
                currentWaypoint = nextWaypoint;
            }

            
        }
        RotateTowardsWaypoint();

    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (currentWaypoint.transform.position - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotateSpeed * Time.deltaTime);

    }

    private void Accelerate()
    {
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += acceleration;
        }
    }

    private void Brake()
    {
        if (moveSpeed > 0f)
        {
            moveSpeed -= brakeForce;
        }
    }

    public void setWaypoint(WaypointEdge waypoint)
    {
        currentWaypoint = waypoint;
    }
    
    
}
