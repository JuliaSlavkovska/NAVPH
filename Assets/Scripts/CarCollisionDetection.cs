using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionDetection : MonoBehaviour
{
    private GameObject car;
    public bool detectCollision = true;
    [SerializeField] private CarMover carMover;
    
    private void OnTriggerEnter(Collider other)
    {
        if (detectCollision && other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Yield"))
        {
            carMover.setBrake(true);            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (detectCollision && other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Yield"))
        {
            carMover.setBrake(true);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car")|| other.gameObject.CompareTag("Yield"))
        {
            carMover.setBrake(false);    
        }

        
    }
}
