using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    float yRotate = 0.0f;

    void Start () {
        speed = 3;
        minAngle = -80.0f;
        maxAngle = 60.0f;
    }
    
    void Update () {
        yRotate += Input.GetAxis ("Mouse X") * speed;
        yRotate = Mathf.Clamp (yRotate, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, yRotate, transform.eulerAngles.z);
    }
}
