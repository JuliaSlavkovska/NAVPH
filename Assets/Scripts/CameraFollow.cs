using UnityEngine;

//script for player camera rotation based on mouse possition
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minAngle = -80.0f;
    [SerializeField] private float maxAngle = 60.0f;

    private float eulerx;
    private float eulerz;
    private float yRotate;
    public bool Freeze { get; private set; }

    private void Start()
    {
        eulerx = transform.eulerAngles.x;
        eulerz = transform.eulerAngles.z;
    }

    private void Update()
    {
        //if game frozen (GameOver scene), stop rotate camera
        if (Freeze is false)
        {
            yRotate += Input.GetAxis("Mouse X") * speed;
            yRotate = Mathf.Clamp(yRotate, minAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(eulerx, yRotate, eulerz);
        }
    }


    //function for setting frozen state
    public void FreezeCam(bool status)
    {
        Freeze = status;
    }
}