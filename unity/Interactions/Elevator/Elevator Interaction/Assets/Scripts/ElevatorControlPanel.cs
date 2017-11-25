using SloanKelly.GameLib;
using UnityEngine;

public class ElevatorControlPanel : MonoBehaviour
{
    bool insideHitBox;
    bool elevatorCalled;

    CanvasGroup infoText;

    public SimpleElevatorMove elevator;

    void Awake()
    {
        infoText = GetComponentInChildren<CanvasGroup>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (elevatorCalled) return;

        insideHitBox = true;
        StartCoroutine(CoroutineFactory.Create(0.25f, time =>
        {
            infoText.alpha = time;
        }));
    }

    void OnTriggerExit(Collider other)
    {
        if (elevatorCalled) return;

        insideHitBox = false;
        StartCoroutine(CoroutineFactory.Create(0.25f, time =>
        {
            infoText.alpha = 1f - time;
        }));
    }

    void Update()
    {
        if (insideHitBox && !elevatorCalled && Input.GetButtonUp("Fire1"))
        {
            elevatorCalled = true;

            elevator.MoveElevator();
            StartCoroutine(CoroutineFactory.Create(1f, time =>
            {
                infoText.alpha = 1f - time;
            }));
        }
    }
}
