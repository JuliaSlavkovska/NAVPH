using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpField : MonoBehaviour
{
    private Color startColor = Color.green;
    private Color sendColor = Color.white;
    private Renderer rend;
    float duration = 1.0f;

    private float lerp;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(startColor, sendColor, lerp);
    }
}
