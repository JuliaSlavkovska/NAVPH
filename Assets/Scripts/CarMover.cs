using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Direction = WaypointEdge.Direction;

public class CarMover : MonoBehaviour
{
    // Uklada referenciu na waypoint system ktory tento objekt pouziva
    [SerializeField] WaypointEdge currentWaypoint;
    [SerializeField] WaypointEdge nextWaypoint;
    public bool turningLeft = false;
    [SerializeField] private float timer = 5f;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public float maxSpeedConstant;
    [SerializeField] public float crossroadSpeedConstant;
    [SerializeField] private float acceleration = 0.03f;
    [SerializeField] private float brakeForce = 0.03f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private CarCollisionDetection detector;
    [SerializeField] private bool brake = false;
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
            if (currentWaypoint.isEntrance)
            {
                // Entering crossroad
                if (!turningLeft)
                    detector.detectCollision = false;
                //Debug.Log(name + " entering. Next waypoint = " + nextWaypoint.name);
                currentWaypoint = nextWaypoint;
            }
            else
            {
                // Exiting crossroad
                detector.detectCollision = true;
                turningLeft = false;
                currentWaypoint = currentWaypoint.GetNext();
                //Debug.Log(name + "_ exiting. Next waypoint = " + currentWaypoint.name);
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
        else if(moveSpeed > maxSpeed)
        {
            moveSpeed -= brakeForce;
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
        //Debug.Log(name + " setting brake to " + value);
        
        brake = value;
    }

    public void setWaypoint(WaypointEdge waypoint)
    {
        currentWaypoint = waypoint;
    }

    public void setNextWaypoint(WaypointEdge waypoint, Direction direction)
    {
        nextWaypoint = waypoint;
        
        if (direction == Direction.Left)
        {
            // Left turn - yield to opposite
            // Blinkers
            turningLeft = true;
            // TODO turn off 
        }
        else if (direction == Direction.Right)
        {
            // Blinkers
        }
        
    }
    
}
