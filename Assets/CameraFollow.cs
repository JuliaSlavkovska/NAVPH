using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    [SerializeField] private float speed = 3;

    void Update () {
        rotation.y += Input.GetAxis ("Mouse X");
        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, rotation.y * speed, transform.eulerAngles.z);
        //transform.eulerAngles = (Vector2)rotation * speed;
    }
}
