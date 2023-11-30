using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightColor
{
    Red,
    YellowToRed,
    YellowToGreen,
    Green
};

public class TrafficLight : MonoBehaviour
{

    [SerializeField] private GameObject red;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject green;
    [SerializeField] private float timer;
    [SerializeField] private float timerMax;
    [SerializeField] private bool startGreen;
    private LightColor currentColor;
    
    public void Start()
    {
        if (startGreen)
        {
            SetGreen();
        }
        else
        {
            SetRed();
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer < timerMax) 
            return;
        timer = 0f;
        switch (currentColor)
        {
            case LightColor.Red:
                SetYellow();
                currentColor = LightColor.YellowToGreen;
                timerMax = 2f;
                break;
            case LightColor.Green:
                SetYellow();
                currentColor = LightColor.YellowToRed;
                timerMax = 2f;
                break;
            case LightColor.YellowToGreen:
                SetGreen();
                timerMax = 10f;
                break;
            case LightColor.YellowToRed:
                SetGreen();
                timerMax = 10f;
                break;
        }

    }

    private void SetRed()
    {
        currentColor = LightColor.Red;
        yellow.SetActive(false);
        green.SetActive(false);
        red.SetActive(true);
    }
    
    private void SetYellow()
    {
        green.SetActive(false);
        red.SetActive(false);
        yellow.SetActive(true);

    }
    
    private void SetGreen()
    {
        currentColor = LightColor.Green;
        yellow.SetActive(false);
        red.SetActive(false);
        green.SetActive(true);

    }

    public LightColor GetColor()
    {
        return currentColor;
    }
        
    

}
