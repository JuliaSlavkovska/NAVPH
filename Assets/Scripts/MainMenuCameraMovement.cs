using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MainMenuCameraMovement : MonoBehaviour
{
    //[SerializeField] private Vector2 min;
    //[SerializeField] private Vector2 max;
    [SerializeField] private float jump = 50f;
    [SerializeField] [Range(0.01f, 0.1f)] private float lerpSpeed = 0.1f;
    private Vector3 _newPosition;



    private void Awake()
    {
        _newPosition = transform.position;
    }

    private void Update()
    {
        _newPosition = new Vector3(transform.position.x+jump, transform.position.y, transform.position.z+jump);
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime*lerpSpeed);
        //transform.position = _newPosition;
        //Debug.Log(transform.position);
        //Debug.Log(_newPosition);
        
        /*
        if (Vector3.Distance(transform.position, _newPosition) < 5f)
        {
            Debug.Log("Nova pozicia");
            GetNewPosition();
        }
        */
        
    }

    void GetNewPosition()
    {
        //_newPosition = new Vector3(transform.position.x+new_jump, transform.position.y, transform.position.z+new_jump);
        _newPosition = new Vector3(transform.position.x+jump, transform.position.y, transform.position.z+jump);
    }
    
}
