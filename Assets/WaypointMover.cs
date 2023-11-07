using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointMover1 : MonoBehaviour
{
    /*
    // Uklada referenciu na waypoint system ktory tento objekt pouziva
    [SerializeField] private Waypoints1 waipoints;

    [SerializeField] private float moveSpeed = 5f;

    [Range(0f,15f)]     //rychlost rotacie playera
    [SerializeField] private float rotateSpeed = 4f;

    [SerializeField] private float distanceTrashold = 0.1f;

    //Aktualny cielovy waypoint tohto objektu
    private Transform currentWaypoint;

    // rotacia pre aktualny frame
    private Quaternion rotationGoal;

    // direction na dalsi waypoint
    private Vector3 directionToWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        // nastavi vychodziu poziciu na 1. waypoint
        currentWaypoint = waipoints.GetNextWaypoint(currentWaypoint);
        //transform.LookAt(currentWaypoint);
        transform.position = currentWaypoint.position;

        //nastavi dalsi waypoint ciel
        currentWaypoint = waipoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceTrashold) {
            currentWaypoint = waipoints.GetNextWaypoint(currentWaypoint);
            //transform.LookAt(currentWaypoint);
        }
        RotateTowardsWaypoint();
    }

    // pomala rotacia playera na direction waypoint
    private void RotateTowardsWaypoint() {
        directionToWaypoint = (currentWaypoint.position - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotationGoal, rotateSpeed * Time.deltaTime);
    
    }

    */
}
