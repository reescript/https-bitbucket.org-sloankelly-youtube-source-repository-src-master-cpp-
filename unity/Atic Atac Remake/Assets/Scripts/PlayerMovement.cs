using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float x;
    float y;
    
    [Tooltip("Speed in pixels / second")]
    public float speed = 104;
    
    void Update()
    {
        x = x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        y = y + Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position = new Vector3((int)x, (int)y, 0);
    }
}