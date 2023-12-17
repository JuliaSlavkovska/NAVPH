using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    
    
    [Header("Lights")]
    [SerializeField] private GameObject LeftBlink;
    [SerializeField] private GameObject RightBlink;
    [SerializeField] private float turnTimerLimit;
    [SerializeField] private float turnTimer;
    [SerializeField] private GameObject EasterEgg;
    bool RightTurn = false;
    bool LeftTurn = false;

    private float timer = 0.4f;
    
    [Header("Speed")]
    [SerializeField]float speed = 0;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationAngle;
    

    private AudioManager _audioManager;
    private ScoreController _scoreController;
    float anglez;
    float anglex;
    
    
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _scoreController = FindObjectOfType<ScoreController>();
        //_audioManager.Play("CarIdle");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_scoreController.getCamStatus())
        {
            //move
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            CarMovement();
            Blinks();
            CarFlip();

        }
    }
    

    void CarFlip()
    {
        anglez = transform.localEulerAngles.z;
        anglex = transform.localEulerAngles.x;

        anglez = (anglez > 180) ? anglez - 360 : anglez;
        anglex = (anglex > 180) ? anglex - 360 : anglex;
        
        //GameOver
        if ((anglez > 30 || anglez < -30 ) || (anglex > 30 || anglex < -30 ) )
        {
            _scoreController.initializeGameOver("Car flip");
        }
    }
    
    

    
    void CarMovement()
    {
        //spomaluj na nulu
        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
        {
            if (speed > 0)
            {
                speed -= 0.05f;
            }else
            {
                speed += 0.05f;
            }
        }
        
        //brake
        if (Input.GetKey(KeyCode.Space))
        {
            if (speed > 0)
            {
                speed -= 0.2f;
            }else
            {
                speed += 0.2f;
            }
        }

        /* Tento zvuk sposoboval praskanie, rozhdoli sme sa ho preto odstranit
         //Zvuk motora
         //Zvuk pri pridanvani plynu
         
        if (Input.GetKeyDown(KeyCode.W))
        {
            //_audioManager.Play("CarMovement");
            //_audioManager.FadeIn("CarMovement", 2, 1f);
            //_audioManager.FadeOut("CarIdle", 2, 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //_audioManager.Stop("CarMovement");
            //_audioManager.FadeOut("CarMovement", 0.5f, 0);
            //_audioManager.FadeIn("CarIdle", 0.5f, 0.6f);
        }
        */

        //WASD
        if (Input.GetKey(KeyCode.W))
        {
            if (speed < 0)
            {
                speed += 0.2f;
            }else 
            {
                speed = Mathf.Clamp(speed + 0.1f, 0, maxSpeed);
            }
        }
        
        if (Input.GetKey(KeyCode.A)) {

            if (speed > 3)
            {
                transform.Rotate(0, -rotationAngle * Time.deltaTime, 0);
                if (turnTimer < turnTimerLimit)
                {
                    turnTimer = Mathf.Clamp(turnTimer + Time.deltaTime, 0, turnTimerLimit);
                    if (Mathf.Approximately(turnTimer, turnTimerLimit))
                    {
                        if (!LeftTurn)
                        {
                            // Turning without a blinker
                            _scoreController.CheckTurn();
                        }                    
                    }
                }
            }

            else if(speed<-3)
                transform.Rotate(0, rotationAngle * Time.deltaTime, 0);
            
        }
        
        if (Input.GetKey(KeyCode.S))
        { 
            if (speed > 0)
            {
                speed -= 0.2f;
            }
            else if(speed >= -1*maxSpeed)
            {
                speed -= 0.1f; 
            }

        }
        
        if (Input.GetKey(KeyCode.D))
        {
            if(speed<-3)
                transform.Rotate(0, -rotationAngle * Time.deltaTime, 0);
            else if (speed > 3)
            {
                transform.Rotate(0, rotationAngle * Time.deltaTime, 0);
                if (turnTimer < turnTimerLimit)
                {
                    turnTimer = Mathf.Clamp(turnTimer + Time.deltaTime, 0, turnTimerLimit);
                    if (Mathf.Approximately(turnTimer, turnTimerLimit))
                    {
                        if (!RightTurn)
                        {
                            _scoreController.CheckTurn();
                        }
                        
                    }
                }            
            }

        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            turnTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            EasterEgg.SetActive(!EasterEgg.activeSelf);
        }
    }

    void Blinks()
    {
        //Right signal
        if (Input.GetKeyDown((KeyCode.Mouse1)))
            SignalLightControl(ref RightTurn, ref LeftTurn, RightBlink, LeftBlink);


        //Left signal
        if (Input.GetKeyDown((KeyCode.Mouse0)))
            SignalLightControl(ref LeftTurn, ref RightTurn,  LeftBlink, RightBlink);          
        
        //flashing when signal is turn on
        if (RightTurn)
            SignalLightFlashing(RightBlink);

        if (LeftTurn)
            SignalLightFlashing(LeftBlink);
    }
    void SignalLightControl(ref bool oneSignal, ref bool secondSignal, GameObject oneblink, GameObject secondblink) {
        //timer = 0.4f;
        //signal on, turn off
        if (oneSignal)
        {
            oneblink.SetActive(false);
            _audioManager.Stop("Indicator");
            oneSignal = false;
        }

        //signal off, turn on && check if already not active the otherone signal if so, shut down
        else
        {
            if (secondSignal)
            {
                secondblink.SetActive(false);
                _audioManager.Stop("Indicator");
                secondSignal = false;
            }
            oneblink.SetActive(true);
            _audioManager.Play("Indicator");
            oneSignal = true;
            timer = 0.4f;
        }

    }
    void SignalLightFlashing(GameObject blink)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            blink.GetComponent<Renderer>().enabled = !blink.GetComponent<Renderer>().enabled ;
            timer = 0.3f;
        }
    }
    
}
