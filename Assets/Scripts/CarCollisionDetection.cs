using UnityEngine;

public class CarCollisionDetection : MonoBehaviour
{
    public bool detectCollision = true;
    [SerializeField] private CarMover carMover;

    private void OnTriggerEnter(Collider other)
    {
        if ((detectCollision && other.gameObject.CompareTag("Car")) ||
            other.gameObject.CompareTag("Yield") ||
            other.gameObject.CompareTag("Player"))
        {
            carMover.setBrake(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car") ||
            other.gameObject.CompareTag("Yield") ||
            other.gameObject.CompareTag("Player"))
            carMover.setBrake(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if ((detectCollision && other.gameObject.CompareTag("Car")) ||
            other.gameObject.CompareTag("Yield") ||
            other.gameObject.CompareTag("Player"))
            carMover.setBrake(true);
    }
}