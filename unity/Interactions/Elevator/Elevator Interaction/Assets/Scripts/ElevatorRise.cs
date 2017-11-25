using UnityEngine;

public class ElevatorRise : MonoBehaviour
{
    bool insideHitBox;
    bool elevatorMoving;

    private void OnTriggerEnter(Collider other)
    {
        insideHitBox = true;
    }

    private void OnTriggerExit(Collider other)
    {
        insideHitBox = false;
    }

    private void Update()
    {
        if (insideHitBox && !elevatorMoving && Input.GetButtonUp("Fire1"))
        {
            elevatorMoving = true;
            GetComponent<SimpleElevatorMove>().MoveElevator();
        }
    }
}
