using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTick : MonoBehaviour
{

    float angleSpeed = -45f;
    
    void Update()
    {
        transform.Rotate(Vector3.forward, angleSpeed * Time.deltaTime);
    }


}
