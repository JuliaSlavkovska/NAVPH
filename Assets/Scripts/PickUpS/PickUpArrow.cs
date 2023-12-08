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
    private Vector3 start;
    private float lerp;

    

    // Update is called once per frame

    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    }

    void Update()
    {
        lerp = Mathf.PingPong(Time.time, speed) / speed;
        transform.position = Vector3.Lerp(start, target, lerp);
    }


}
