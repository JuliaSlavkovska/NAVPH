using UnityEngine;

public class PickUpArrow : MonoBehaviour
{
    private float lerp;

    // Start is called before the first frame update
    private float movementValue;
    private float speed = 1f;
    private Vector3 start;
    private Vector3 target;
    private float value = 3f;


    // Update is called once per frame

    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        lerp = Mathf.PingPong(Time.time, speed) / speed;
        transform.position = Vector3.Lerp(start, target, lerp);
        transform.localRotation =
            Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 0.2f, transform.eulerAngles.z);
    }
}