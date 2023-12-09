using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{

    public int Score { get; private set; }
    public float Health { get; private set; }
    public float damage { get; private set; }

    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;



    void Start()
    {
        Health = 1f;
        Score = 0;
        damage = 0.05f;
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

    public void RestartGame()
    {
        Score = 0;
        Health = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
    public void BackToMenu()
    {
        Score = 0;
        Health = 1;
        SceneManager.LoadScene(0);
        
    }


}
