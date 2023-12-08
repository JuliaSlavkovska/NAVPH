using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpPoint : MonoBehaviour
{
    private ScoreController _scoreController;
    public UnityEvent OnPickup;

    private void Awake()
    {
        _scoreController = FindObjectOfType<ScoreController>();
    }
    
    /*
    public void DestinationReached()
    {
        OnPickup.Invoke();
    }
    */
    
}
