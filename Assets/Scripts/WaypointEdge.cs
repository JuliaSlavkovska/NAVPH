using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointEdge : MonoBehaviour
{
    [SerializeField] private WaypointEdge[] directions;
    [ColorUsage(false, true)] public Color color = Color.blue;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, 1f);

        Gizmos.color = Color.green;

        if (directions != null)
        {
            for (int i = 0; i <= directions.Length - 1; i++)
            {
                Gizmos.DrawLine(transform.position, directions[i].transform.position);
            }
        }
    }

    public WaypointEdge GetNext()
    {
        if (directions.Length > 0)
        {
            int randomNumber = Random.Range(0, directions.Length);
            
            return directions[randomNumber];
        }
        else
        {
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
