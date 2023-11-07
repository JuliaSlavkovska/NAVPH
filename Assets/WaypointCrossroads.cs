using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCrossroads : MonoBehaviour
{

    [SerializeField] private WaypointEdge[] waypointEdges;
    // Start is called before the first frame update

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        
        Gizmos.color = Color.magenta;

        if (waypointEdges != null)
        {

            for (int i = 0; i <= waypointEdges.Length - 1; i++)
            {
                Gizmos.DrawLine(transform.position, waypointEdges[i].transform.position);
            }
        }
        
    }
        


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
