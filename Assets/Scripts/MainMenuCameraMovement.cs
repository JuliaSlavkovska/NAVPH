using UnityEngine;

//script for camera movement above town, used in Menu scene
public class MainMenuCameraMovement : MonoBehaviour
{
    [SerializeField] private float jump = 50f;
    [SerializeField] [Range(0.01f, 0.1f)] private float lerpSpeed = 0.1f;
    private Vector3 _newPosition;


    private void Awake()
    {
        _newPosition = transform.position;
    }

    private void Update()
    {
        _newPosition = new Vector3(transform.position.x+jump, transform.position.y, transform.position.z+jump);
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime*lerpSpeed);
    }
    
}
