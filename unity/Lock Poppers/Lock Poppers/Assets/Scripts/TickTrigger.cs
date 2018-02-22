using System;
using UnityEngine;

public class TickTrigger : MonoBehaviour
{
    enum TickState
    {
        Moving,
        InsideBall,
        ExitingBall
    }

    TickState state = TickState.Moving;

    public bool InsideBall { get { return state == TickState.InsideBall; } }

    public bool MissedBall { get { return state == TickState.ExitingBall; } }

    public Action tickExit;

    public void Reset()
    {
        state = TickState.Moving;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        state = TickState.InsideBall;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        state = TickState.ExitingBall;
        if (tickExit != null)
        {
            tickExit();
        }
    }
}
