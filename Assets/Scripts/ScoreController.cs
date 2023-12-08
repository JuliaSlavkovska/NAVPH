using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{

    public int Score { get; private set; }
    public UnityEvent OnScoreChanged;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("Trigger with PickupPoint");
            Score++;
            Debug.Log(Score);
            OnScoreChanged.Invoke();
        }
    }

}
