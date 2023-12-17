using UnityEngine;

public class PickUpField : MonoBehaviour
{
    private float duration = 1.0f;

    private float lerp;
    private Renderer rend;
    private Color sendColor = Color.white;

    private Color startColor = Color.green;

    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(startColor, sendColor, lerp);
    }
}