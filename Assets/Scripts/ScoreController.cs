using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{

    public int Score { get; private set; }
    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Score++;
            OnScoreChanged.Invoke();
            OnPickUpReached.Invoke();
        }
    }

}
