using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class CarMover : MonoBehaviour
{
    // Uklada referenciu na waypoint system ktory tento objekt pouziva
    private WaypointEdge currentWaypoint;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 0.03f;
    [SerializeField] private float brakeForce = 0.03f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private CarCollisionDetection detector;
    private bool brake = false;
    private Quaternion rotationGoal;
    private Vector3 directionToWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        //nastavi dalsi waypoint ciel
        //currentWaypoint = currentWaypoint.GetNext();
        //transform.position = currentWaypoint.transform.position;
        currentWaypoint = currentWaypoint.GetNext();
        transform.LookAt(currentWaypoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (brake == false)
        {
            Accelerate();
        }
        else
        {
            Brake();
        }
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < 0.1f)
        {
            // Reached the current target
            // Waypoint is an entry to the crossroad - pass without stopping for anything
            detector.detectCollision = !currentWaypoint.isEntrance;
            currentWaypoint = currentWaypoint.GetNext();
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
            moveSpeed = Mathf.Clamp(moveSpeed - brakeForce, 0f, maxSpeed);
        }
    }

    public void setBrake(bool value)
    {
        brake = value;
    }

    public void setWaypoint(WaypointEdge waypoint)
    {
        currentWaypoint = waypoint;
    }
    
    
}
