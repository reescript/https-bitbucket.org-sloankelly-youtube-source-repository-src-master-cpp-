using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public RotateBall ballPivot;

    public GameObject tickPivot;

    private void Start()
    {
        ballPivot.StartRotation(ShowTick);
    }
    
    private void ShowTick()
    {
        tickPivot.SetActive(true);
    }
}
