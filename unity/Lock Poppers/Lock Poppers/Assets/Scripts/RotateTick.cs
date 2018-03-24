using System;
using UnityEngine;

public class RotateTick : MonoBehaviour
{
    private bool rotating = true;

    public float angleSpeed = -45f;

    private TickTrigger tickTrigger;

    public Action missedTheBall;

    public float direction = 1f;

    public float ZRotation { get { return transform.rotation.eulerAngles.z; } }

    public bool InsideBall { get { return tickTrigger == null ? false : tickTrigger.InsideBall; } }

    public void Rotate(bool rotate)
    {
        rotating = rotate;
    }
    
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
        if (rotating)
        {
            transform.Rotate(Vector3.forward, angleSpeed * Time.deltaTime * direction);
        }
    }

    void MissedTheBall()
    {
        if (missedTheBall != null)
        {
            missedTheBall();
        }
    }
}
