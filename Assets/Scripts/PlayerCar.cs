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
    [SerializeField] private GameObject RightTurns;
    [SerializeField] private GameObject LeftTurns;
    [SerializeField] private GameObject LeftBlink;
    [SerializeField] private GameObject RightBlink;
    [SerializeField] private GameObject objekt;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private float turnTimerLimit;
    [SerializeField] private float turnTimer;
    
    
    

    bool RightTurn = false;
    bool LeftTurn = false;
    //private bool Freeze = false;
    private float timer;
    private List<Transform> rlights = new List<Transform>();
    private List<Transform> llights = new List<Transform>();
    
    [Header("Speed")]
    [SerializeField]float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationAngle;
    

    private AudioManager _audioManager;
    private ScoreController _scoreController;
    float anglez;
    float anglex;
    
    
    void Start()
    {
        foreach (Transform light_signal in RightTurns.transform)
        {
            light_signal.GetComponent<Light>().enabled = false;
            rlights.Add(light_signal);
        }
        foreach (Transform light_signal in LeftTurns.transform)
        {
            light_signal.GetComponent<Light>().enabled = false;
            llights.Add(light_signal);
        }
        LeftBlink.SetActive(false);
        RightBlink.SetActive(false);

        speed =0f;
        timer = 0.4f;
        _audioManager = FindObjectOfType<AudioManager>();
        _scoreController = FindObjectOfType<ScoreController>();
        _audioManager.Play("CarIdle");
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
        if ((anglez > 20 || anglez < -20 ) || (anglex > 20 || anglex < -20 ) )
        {
            //objekt.GetComponent<ScoreController>().RestartGame();
        }
    }
    
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Yield"))
        {
            Debug.Log("Score deducted");
        }
        else if(other.CompareTag("Car") || other.CompareTag("Prop"))
        {
            Debug.Log("Game Over");
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            //_audioManager.Play("CarMovement");
            _audioManager.FadeIn("CarMovement", 2, 1f);
            _audioManager.FadeOut("CarIdle", 2, 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //_audioManager.Stop("CarMovement");
            _audioManager.FadeOut("CarMovement", 0.5f, 0);
            _audioManager.FadeIn("CarIdle", 0.5f, 0.6f);
        }

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
                            Debug.Log("Illegal turning detected!");
                        }                    }
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
                            Debug.Log("Illegal turning detected!");
                        }
                        
                    }
                }            
            }

        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            turnTimer = 0;
        }
    }

    void Blinks()
    {
        //Right signal
        if (Input.GetKeyDown((KeyCode.Mouse1)))
            SignalLightControl(ref RightTurn, rlights, ref LeftTurn, llights, RightBlink, LeftBlink);


        //Left signal
        if (Input.GetKeyDown((KeyCode.Mouse0)))
            SignalLightControl(ref LeftTurn, llights, ref RightTurn, rlights, LeftBlink, RightBlink);          
        
        //flashing when signal is turn on
        if (RightTurn)
            SignalLightFlashing(rlights, RightBlink);

        if (LeftTurn)
            SignalLightFlashing(llights, LeftBlink);
    }
    void SignalLightControl(ref bool oneSignal, List<Transform> oneSignals, ref bool secondSignal, List<Transform> secondSignals, GameObject oneblink, GameObject secondblink) {
        //timer = 0.4f;
        //signal on, turn off
        if (oneSignal)
        {
            foreach (Transform signal in oneSignals)
            {
                signal.GetComponent<Light>().enabled = false;
            }
            oneblink.SetActive(false);
            _audioManager.Stop("Indicator");
            oneSignal = false;
        }

        //signal off, turn on && check if already not active the otherone signal if so, shut down
        else
        {
            if (secondSignal)
            {
                foreach (Transform signal in secondSignals)
                {
                    signal.GetComponent<Light>().enabled = false;
                }
                secondblink.SetActive(false);
                _audioManager.Stop("Indicator");
                secondSignal = false;
            }

            foreach (Transform signal in oneSignals)
            {
                signal.GetComponent<Light>().enabled = true;
            }
            oneblink.SetActive(true);
            _audioManager.Play("Indicator");
            oneSignal = true;
            timer = 0.4f;
        }

    }
    void SignalLightFlashing(List<Transform> lights, GameObject blink)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            foreach (Transform light_signal in lights)
            {
                light_signal.GetComponent<Light>().enabled = !light_signal.GetComponent<Light>().enabled;
            }
            blink.GetComponent<Renderer>().enabled = !blink.GetComponent<Renderer>().enabled ;
            timer = 0.3f;
        }
    }
    
}
