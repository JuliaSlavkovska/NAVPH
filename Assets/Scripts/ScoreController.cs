using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{

    public static ScoreController instance;
    
    public int Delivery { get; private set; }
    public float Health { get; private set; }
    public string BrokenRule { get; private set; }
    public float BrokenRules { get; private set; }
    
    [SerializeField] private CameraFollow camera;

    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;
    public UnityEvent OnCrash;
    public UnityEvent OnBrokenRule;
    
    private Ray ray;
    private float _start_time;


    private AudioManager _audioManager;
    private MenuController _menuController;
    private float damage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Health = 1f;
        Delivery = 0;
        damage = 0.05f;
        BrokenRules = 0;
        _audioManager = FindObjectOfType<AudioManager>();
        _menuController = FindObjectOfType<MenuController>();
        
    }
    
    private void Update()
    {
        if (!camera.Freeze)
        {
            CheckOnTrack();
        }
    }

    public void CheckTurn()
    {
        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit) && !hit.collider.CompareTag("Turn"))
        {
            RuleBroken("Blinker not activated");
        }
    }

    private void CheckOnTrack()
    {
        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.CompareTag("Grass"))
            {
                if (Time.time - _start_time > 1)
                {
                    RuleBroken("Get back on road!");
                    _start_time = Time.time;
                }
                
                OnScoreChanged.Invoke();
            }
            if(hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Turn"))
            {
                _start_time = Time.time;
            }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag("PickUp"))
        {
            Delivery++;
            _audioManager.Play("PickUp");
            OnScoreChanged.Invoke();
            OnPickUpReached.Invoke();
        }
        else if (other.CompareTag("Yield"))
        {
            Debug.Log("Rule broken");
            RuleBroken("You didn't give the right of way!");        
        }
        
        else if (other.CompareTag("Cube") || 
            other.CompareTag("Car") ||
            other.CompareTag("Prop"))
        {
            initializeGameOver("Crash!");
        }

    }

    
    //dorobit kedy sa realne ma odratat zdravie
    public void RuleBroken(string reason)
    {
        BrokenRules++;
        Health-=damage;
        _audioManager.Play("Fail");
        OnScoreChanged.Invoke();
        BrokenRule = reason;
        OnBrokenRule.Invoke();
        //Game Over
        if (Health <= 0)
        {
            initializeGameOver("Out of lives!");
        }

    }

    public void initializeGameOver(string reason)
    {
        _menuController.GameOver(reason);
        OnCrash.Invoke();
    }

    public void RestoreScore()
    {
        Delivery = 0;
        Health = 1;
    }

    public void FreezeCam(bool status)
    {
        camera.FreezeCam(status);
    }
    
    public bool getCamStatus()
    {
        return camera.Freeze;
    }

}
