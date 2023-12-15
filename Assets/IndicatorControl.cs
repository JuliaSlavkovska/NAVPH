using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorControl : MonoBehaviour
{
    [SerializeField] private List<Transform> rlights;
    [SerializeField] private List<Transform> llights;
    private float timer;
    private bool RightTurn;
    private bool LeftTurn;

    private void Update()
    {
        if (LeftTurn)
        {
            
        }

        if (RightTurn)
        {
            
        }
    }

    public void LeftBlink(bool state)
    {
        if (state)
        {
            foreach (Transform signal in llights)
            {
                signal.GetComponent<Light>().
            }

            LeftTurn = true;
        }
        else
        {
            foreach (Transform signal in llights)
            {
                signal.GetComponent<Light>().enabled = false;
            }

            LeftTurn = false;
        }
        
    }
    
    void Blinks()
    {
        //Right signal
        if (Input.GetKeyDown((KeyCode.Mouse1)))
            SignalLightControl(ref RightTurn, rlights, ref LeftTurn, llights);


        //Left signal
        if (Input.GetKeyDown((KeyCode.Mouse0)))
            SignalLightControl(ref LeftTurn, llights, ref RightTurn, rlights);          
        
        //flashing when signal is turn on
        if (RightTurn)
            SignalLightFlashing(rlights);

        if (LeftTurn)
            SignalLightFlashing(llights);
    }
    void SignalLightControl(ref bool oneSignal, List<Transform> oneSignals, ref bool secondSignal, List<Transform> secondSignals) {

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
            timer = 0.3f;
        }
    }
}