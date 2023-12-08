using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    //[SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;
    private PickUpField target;
    [SerializeField] private AllPickUps AllPickUpsScript;
    [SerializeField] private PickUpField startingPickUp;

    // Start is called before the first frame update
    void Start()
    {
        NextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.transform.position-transform.position),rotationSpeed*Time.deltaTime);
        //Vector3 tempRot= transform.localEulerAngles;
        //tempRot.x=0;
        //transform.localEulerAngles=tempRot;
    }

    public void NextDestination()
    {
        target = AllPickUpsScript.GetActuallPickUpField();
    }
}
