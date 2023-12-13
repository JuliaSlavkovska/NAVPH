using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{

    public bool Freeze { get; private set; }
    public int Delivery { get; private set; }
    public float Health { get; private set; }
    public float damage { get; private set; }

    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;
    public UnityEvent OnCrash;
    
    [SerializeField] private CameraFollow camera;

    private void Awake()
    {
        Freeze = false;
    }

    void Start()
    {
        Health = 1f;
        Delivery = 0;
        damage = 0.05f;
        Freeze = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Delivery++;
            OnScoreChanged.Invoke();
            OnPickUpReached.Invoke();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Cube"))
        {
            Freeze = true;
            camera.FreezeCam();
            OnCrash.Invoke();
        }

    }
    
    //dorobit kedy sa realne ma odratat zdravie
    public void RuleBroken()
    {
        Health-=damage; 
        //TODO aktualizovat HUD
    }
    
    
    
    //premiestnot nejakoa si do GameCOntroller

    public void RestartGame()
    {
        Delivery = 0;
        Health = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
    public void BackToMenu()
    {
        Delivery = 0;
        Health = 1;
        SceneManager.LoadScene(0);
        
    }


}
