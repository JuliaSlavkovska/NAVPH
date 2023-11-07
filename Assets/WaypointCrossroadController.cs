using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCrossroadController : MonoBehaviour
{
    [SerializeField] public WaypointCrossroads[] waipoints;
    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
 

        Gizmos.color = Color.magenta;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(0).position, transform.GetChild(i + 1).position);
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
