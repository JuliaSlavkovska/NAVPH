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
    public float BrokenRules { get; private set; }
    public string GameOverReason { get; private set; }

    public UnityEvent OnScoreChanged;
    public UnityEvent OnPickUpReached;
    public UnityEvent OnCrash;
    
    private Ray ray;
    private float _start_time;


    
    [SerializeField] private CameraFollow camera;
    private AudioManager _audioManager;
    private float damage;
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
        BrokenRules = 0;
        _audioManager = FindObjectOfType<AudioManager>();
        
    }

    private void Update()
    {
        CheckOnTrack();
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
            Freeze = true;
            camera.FreezeCam();
            GameOverReason = "Crash!";
            OnCrash.Invoke();
        }

    }
    
    //dorobit kedy sa realne ma odratat zdravie
    private void RuleBroken()
    {
        Health-=damage;
        
        //Game Over
        if (Health <= 0)
        {
            Freeze = true;
            camera.FreezeCam();
            GameOverReason = "Out of lives!";
            OnCrash.Invoke();
        }

    }
    
    
    
    //premiestnot nejako asi do GameController

    public void RestartGame()
    {
        Delivery = 0;
        Health = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
    //premiestnot nejako asi do GameController
    public void BackToMenu()
    {
        Delivery = 0;
        Health = 1;
        SceneManager.LoadScene(0);
        
    }


}
