using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement test.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // x and y co-ordinates.
    float x;
    float y;
    
    [Tooltip("Speed in pixels / second")]
    public float speed = 104;
    
    /// <summary>
    /// Update the player's movement.
    /// </summary>
    void Update()
    {
        x = x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        y = y + Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position = new Vector3((int)x, (int)y, 0);
    }
}