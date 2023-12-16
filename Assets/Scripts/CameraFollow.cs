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

    private float eulerx; 
    private float eulerz; 
    public bool Freeze { get; private set; }

    void Start () {
        speed = 3;
        minAngle = -80.0f;
        maxAngle = 60.0f;
        eulerx = transform.eulerAngles.x;
        eulerz = transform.eulerAngles.z;
    }

    void Update()
    {
        if (Freeze is false)
        {
            yRotate += Input.GetAxis("Mouse X") * speed;
            yRotate = Mathf.Clamp(yRotate, minAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(eulerx, yRotate, eulerz);
        }
        else
        {
            Debug.Log("V else");
        }
    }

    public void FreezeCam(bool status)
    {
        Freeze = status;
    }
}
