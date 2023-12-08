using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpArrow : MonoBehaviour
{
    // Start is called before the first frame update
    private float movementValue;
    float elapsed = 0f;
    float value = 8f;
    float speed = 1f;
    private Vector3 target;
    

    // Update is called once per frame

    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
    }

    void Update()
    {
        
        elapsed += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime*speed);
        
        if (elapsed >= 1f) {
            value*=-1f;
            elapsed %=1f;
            target = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
        }

    }


}
