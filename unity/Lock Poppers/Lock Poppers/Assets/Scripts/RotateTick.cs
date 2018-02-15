using System;
using UnityEngine;

public class RotateTick : MonoBehaviour
{
    public float angleSpeed = -45f;

    private TickTrigger tickTrigger;

    public Action missedTheBall;

    public float direction = 1f;

    public bool InsideBall { get { return tickTrigger == null ? false : tickTrigger.InsideBall; } }

    public void Reset()
    {
        tickTrigger.Reset();
    }

    void Awake()
    {
        tickTrigger = GetComponentInChildren<TickTrigger>();
        tickTrigger.tickExit = MissedTheBall;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, angleSpeed * Time.deltaTime * direction);
    }

    void MissedTheBall()
    {
        if (missedTheBall != null)
        {
            missedTheBall();
        }
    }
}
