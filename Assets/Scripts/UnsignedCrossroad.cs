using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnsignedCrossroad: Crossroad
{
    public void Update()
    {
        foreach (var waypoint in waypoints)
        {
            if (waypoint.leftYield == null)
                continue;
            if (waypoint.numOfCars > 0)
            {
                // Block the direction on my left
                // (they have to yield to cars on their right)
                waypoint.leftYield.blocker.transform.localPosition = Vector3.zero;
            }
            else
            {
                // Unblock the left direction
                waypoint.leftYield.blocker.transform.localPosition = new Vector3(0, 100, 0);
            }
        }
    }
}