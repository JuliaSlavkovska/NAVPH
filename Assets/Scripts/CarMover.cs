using UnityEngine;

public class CarMover : MonoBehaviour
{
    // Uklada referenciu na waypoint system ktory tento objekt pouziva
    [SerializeField] private WaypointEdge currentWaypoint;
    [SerializeField] private WaypointEdge nextWaypoint;
    public bool turningLeft;
    [SerializeField] private float timer = 5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public float maxSpeedConstant;
    [SerializeField] public float crossroadSpeedConstant;
    [SerializeField] private float acceleration = 0.03f;
    [SerializeField] private float brakeForce = 0.03f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private CarCollisionDetection detector;
    [SerializeField] private IndicatorControl indicatorControl;
    [SerializeField] private bool brake;
    private Vector3 directionToWaypoint;

    private Quaternion rotationGoal;

    // Start is called before the first frame update
    private void Start()
    {
        //nastavi dalsi waypoint ciel
        //currentWaypoint = currentWaypoint.GetNext();
        //transform.position = currentWaypoint.transform.position;
        currentWaypoint = currentWaypoint.GetNext();
        transform.LookAt(currentWaypoint.transform);
    }

    // Update is called once per frame
    private void Update()
    {
        if (brake == false)
            Accelerate();
        else
            Brake();
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position,
            moveSpeed * Time.deltaTime);

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
                indicatorControl.LeftBlink(false);
                indicatorControl.RightBlink(false);
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

    public void setNextWaypoint(WaypointEdge waypoint, WaypointEdge.Direction direction)
    {
        nextWaypoint = waypoint;

        if (direction == WaypointEdge.Direction.Left)
        {
            // Left turn - yield to opposite
            // Blinkers
            indicatorControl.LeftBlink(true);
            turningLeft = true;
        }
        else if (direction == WaypointEdge.Direction.Right)
        {
            // Blinkers
            indicatorControl.RightBlink(true);
        }
    }
}