using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionDetection : MonoBehaviour
{
    private GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        car = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            car.GetComponent<CarMover>().setBrake(true);            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            car.GetComponent<CarMover>().setBrake(false);    
        }

        
    }
}
