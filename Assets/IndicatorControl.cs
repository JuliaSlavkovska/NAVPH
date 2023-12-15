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
            SignalLightFlashing(llights);
        }

        if (RightTurn)
        {
            SignalLightFlashing(rlights);
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

    public void LeftBlink(bool state)
    {
        if (state)
        {
            foreach (Transform signal in llights)
            {
                signal.GetComponent<Light>().enabled = true;
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
    
    public void RightBlink(bool state)
    {
        if (state)
        {
            foreach (Transform signal in rlights)
            {
                signal.GetComponent<Light>().enabled = true;
            }

            RightTurn = true;
        }
        else
        {
            foreach (Transform signal in rlights)
            {
                signal.GetComponent<Light>().enabled = false;
            }

            RightTurn = false;
        }
        
    }

}