using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CarControll : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private GameObject RightTurns;
    [SerializeField] private GameObject LeftTurns;

    bool RightTurn = false;
    bool LeftTurn = false;
    private float timer;
    private List<Transform> rlights = new List<Transform>();
    private List<Transform> llights = new List<Transform>();

    // Start is called before the first frame update
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


        timer = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
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
