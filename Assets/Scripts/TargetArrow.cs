using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.position-transform.position),rotationSpeed*Time.deltaTime);
        //Vector3 tempRot= transform.localEulerAngles;
        //tempRot.x=0;
        //transform.localEulerAngles=tempRot;
    }
}
