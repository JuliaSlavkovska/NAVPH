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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Yield"))
        {
            transform.parent.gameObject.GetComponent<CarMover>().setBrake(true);            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Yield"))
        {
            Debug.Log(transform.parent.gameObject.name + " detecting a " + other.gameObject.name + ", stopping!");
            transform.parent.gameObject.GetComponent<CarMover>().setBrake(true);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car")|| other.gameObject.CompareTag("Yield"))
        {
            transform.parent.gameObject.GetComponent<CarMover>().setBrake(false);    
        }

        
    }
}
