using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{

    public int Score { get; private set; }
    public float Health { get; private set; }
    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;

    void Start()
    {
        Health = 1f;
        Score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Debug.Log(Health);
            Score++;
            Health-=0.05f; //dorobit kedy sa realne ma odratat zdravie
            Debug.Log(Health);
            OnScoreChanged.Invoke();
            OnPickUpReached.Invoke();
        }
    }

}
