using System.Collections.Generic;
using UnityEngine;

public class SignsCrossroad : Crossroad
{
    [SerializeField] private List<GameObject> yieldBlockers = new();
    [SerializeField] public int carsOnMain;
    [SerializeField] private bool blocking;

    public void Update()
    {
        carsOnMain = 0;
        foreach (var waypoint in waypoints) 
            carsOnMain += waypoint.numOfCars;
        if (!blocking && carsOnMain > 0)
        {
            // block 
            blocking = true;
            foreach (var blocker in yieldBlockers) 
                blocker.transform.localPosition = Vector3.zero;
        }
        else if (blocking && carsOnMain == 0)
        {
            // unblock 
            blocking = false;
            foreach (var blocker in yieldBlockers) 
                blocker.transform.localPosition = new Vector3(0, 100, 0);
        }
    }
}