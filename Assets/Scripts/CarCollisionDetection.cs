using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionDetection : MonoBehaviour
{
    public bool detectCollision = true;
    [SerializeField] private CarMover carMover;
    
    private void OnTriggerEnter(Collider other)
    {
        if (detectCollision && other.gameObject.CompareTag("Car") || 
            other.gameObject.CompareTag("Yield") ||
            other.gameObject.CompareTag("Player")) 
        {
            Debug.Log(carMover.name + " colliding with " + other.name);
            carMover.setBrake(true);            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (detectCollision && other.gameObject.CompareTag("Car") || 
            other.gameObject.CompareTag("Yield") ||
            other.gameObject.CompareTag("Player"))
        {
            Debug.Log(carMover.name + " colliding with " + other.name);
            carMover.setBrake(true);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car") || 
            other.gameObject.CompareTag("Yield") ||
            other.gameObject.CompareTag("Player")) 
        {
            Debug.Log(carMover.name + " ended collision with " + other.name);
            carMover.setBrake(false);    
        }

        
    }
}
