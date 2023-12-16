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
    public float BrokenRules { get; private set; }
    
    [SerializeField] private CameraFollow camera;

    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;
    public UnityEvent OnCrash;
    
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

    public void FreezeCam(bool status)
    {
        camera.FreezeCam(status);
    }
    
    public bool getCamStatus()
    {
        return camera.Freeze;
    }

    private void Update()
    {
        if (!camera.Freeze)
        {
            CheckOnTrack();
        }
    }

    private void CheckOnTrack()
    {
        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.tag == "Grass")
            {
                if (Time.time - _start_time > 1)
                {
                    RuleBroken();
                    _start_time = Time.time;
                }
                
                OnScoreChanged.Invoke();
            }
            if(hit.collider.tag == "Ground")
            {
                _start_time = Time.time;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Delivery++;
            _audioManager.Play("PickUp");
            OnScoreChanged.Invoke();
            OnPickUpReached.Invoke();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.CompareTag("Cube"))
            {
                _audioManager.Play("Crash");
                _menuController.GameOver("Crash!");
                OnCrash.Invoke();
            }

    }

    
    //dorobit kedy sa realne ma odratat zdravie
    private void RuleBroken()
    {
        Health-=damage;
        _audioManager.Play("Fail");
        
        //Game Over
        if (Health <= 0)
        {
            _menuController.GameOver("Out of lives!!");
            OnCrash.Invoke();
        }

    }
    

    public void RestoreScore()
    {
        Delivery = 0;
        Health = 1;
    }


}
