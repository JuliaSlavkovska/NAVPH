using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoints1 : MonoBehaviour
{
    /*
    [Range(0f,2f)]
    [SerializeField] private float waypointSize = 1f;

    [Header("Nastavenia cesty")]

    // nastavi cestu aby cyklila
    [SerializeField] private bool canLoop = true;

    //vpred alebo vzad
    [SerializeField] private bool isMovingForward = true;
    
    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        // ak je casta loopovana, nakresli cesty medzi zaciatkom aj koncom
        if (canLoop)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
        }
    }

    //ziska dalsii waypoint na zaklade aktualneho smeru
    public Transform GetNextWaypoint(Transform currentWaypoint) {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        //Stores the index of current waypoint
        int currentIndex = currentWaypoint.GetSiblingIndex();

        //Stores index of next waypoint
        int nextIndex = currentIndex;


        // Player ide vpred
        if (isMovingForward)
        {
            nextIndex += 1;

            // If next waypoint index == count of children/waypoint, than it is already at the last waypoint
            // check if path is set to loop and return 1.waypoint as the current waypoint, otherwise subtrack 1
            // from nexIndex which will return same waypoint that the agent currently is, that will stop the moving

            if (nextIndex == transform.childCount)
            {
                if (canLoop)
                {
                    nextIndex = 0;
                }
                else
                {
                    nextIndex = -1;
                }
            }
        }

        //Player ide vzad
        else {
            nextIndex -= 1;

            //if nexIndex below 0, it means you are already at the first waypoint.
            //checked if path is set to loop and if so return the last waypoint, otherwise add nextIndex+1 which
            // will return the current waypoint that you are already at which will cause agent to stop, because he is already there
            if (nextIndex < 0)
            {
                if (canLoop)
                {
                    nextIndex = transform.childCount - 1;
                }
                else
                {
                    nextIndex += 1;
                }
            }
        }

        return transform.GetChild(nextIndex);

        
    }

    */
}
