using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{

    [SerializeField] private GameObject red;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject green;


    public void SetRed()
    {
        yellow.SetActive(false);
        green.SetActive(false);
        red.SetActive(true);
    }
    
    public void SetYellow()
    {
        green.SetActive(false);
        red.SetActive(false);
        yellow.SetActive(true);

    }
    
    public void SetGreen()
    {
        yellow.SetActive(false);
        red.SetActive(false);
        green.SetActive(true);

    }

}
