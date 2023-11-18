using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Lights")]
    [SerializeField] private GameObject RightTurns;
    [SerializeField] private GameObject LeftTurns;

    bool RightTurn = false;
    bool LeftTurn = false;
    private float timer;
    private List<Transform> rlights = new List<Transform>();
    private List<Transform> llights = new List<Transform>();
 
    
    [Header("Speed")]
    [SerializeField]float speed;
    [SerializeField] float maxSpeed = 30.0f;
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

        speed =0f;
        timer = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
        //spomaluj na nulu
        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
        {
            if (speed > 0)
            {
                speed -= 0.1f;
            }else
            {
                speed += 0.1f;
            }
        }
        
        //rucna
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

        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            if (speed < 0)
            {
                speed += 0.2f;
            }else if (speed <= maxSpeed)
            {
                speed += 0.1f;
            }
        }
        
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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


        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)) {
            
            if(speed>3)
                transform.Rotate(0, -0.5f, 0);
            else if(speed<-3)
                transform.Rotate(0, 0.5f, 0);
            
        }
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            if(speed<-3)
                transform.Rotate(0, -0.5f, 0);
            else if(speed>3)
                transform.Rotate(0, 0.5f, 0);
            
        }
        
        //Right signal
        if (Input.GetKeyDown((KeyCode.Keypad6)))
        {
            SignalLightControl(ref RightTurn, rlights, ref LeftTurn, llights);
        }

        //Left signal
        if (Input.GetKeyDown((KeyCode.Keypad4)))
        {
            SignalLightControl(ref LeftTurn, llights, ref RightTurn, rlights);          
        }


        //flashing when signal is turn on
        if (RightTurn)
        {
            SignalLightFlashing(rlights);
        }
        if (LeftTurn)
        {
            SignalLightFlashing(llights);
        }
        
    }
    
    void SignalLightControl(ref bool oneSignal, List<Transform> oneSignals, ref bool secondSignal, List<Transform> secondSignals) {
        timer = 0.4f;
        //signal on, turn off
        if (oneSignal)
        {
            foreach (Transform signal in oneSignals)
            {
                signal.GetComponent<Light>().enabled = false;
            }
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
                secondSignal = false;
            }

            foreach (Transform signal in oneSignals)
            {
                signal.GetComponent<Light>().enabled = true;
            }
            oneSignal = true;
            timer = 0.4f;
        }

    }
     

    void SignalLightFlashing(List<Transform> lights)
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
            timer = 0.4f;
        }
    }
}
