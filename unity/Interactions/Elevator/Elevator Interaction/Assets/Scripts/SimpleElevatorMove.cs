using SloanKelly.GameLib;
using System.Collections;
using UnityEngine;

public class SimpleElevatorMove : MonoBehaviour
{
    bool onStartFloor = true;
    bool running;

    Vector3 endPostion;
    
    Vector3 startPostion;

    public float destinationHeight;

    public float duration = 3;

    public void MoveElevator()
    {
        if (running) return;
        StartCoroutine(MoveElevator(onStartFloor));
    }

    void Awake()
    {
        startPostion = transform.position;
        endPostion = startPostion + new Vector3(0, destinationHeight, 0);
    }
    
    private IEnumerator MoveElevator(bool onGroundFloor)
    {
        Vector3 start = onGroundFloor ? startPostion : endPostion;
        Vector3 end = onGroundFloor ? endPostion : startPostion;

        running = true;

        return CoroutineFactory.Create(duration, time =>
        {
            transform.position = Vector3.Lerp(start, end, time);
        },
        () =>
        {
            running = false;
            this.onStartFloor = !this.onStartFloor;
        });
    }
}
