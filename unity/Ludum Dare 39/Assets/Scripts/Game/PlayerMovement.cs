using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameController gameController;

    Vector2 mouseStart = Vector2.zero;
    Vector2 mouseEnd = Vector2.zero;

    public PlayerAnimationController animationController;

    bool wasMoving = false;

    void Start()
    {
        StartCoroutine(KeyboardUpdate());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseEnd = Input.mousePosition;

            var diff = (mouseEnd-mouseStart).normalized;

            if (diff.y > 0.8f && Mathf.Abs(diff.x) < 0.5f)
            {
                gameController.MoveNorth();
            }
            else if (diff.y < -0.8f && Mathf.Abs(diff.x) < 0.5f)
            {
                gameController.MoveSouth();
            }
            else if (diff.x > 0.8f && Mathf.Abs(diff.y) < 0.5f)
            {
                gameController.MoveEast();
            }
            else if (diff.x < -0.8f && Mathf.Abs(diff.y) < 0.5f)
            {
                gameController.MoveWest();
            }
        }
    }
    
    IEnumerator KeyboardUpdate()
    {
        while (true)
        {
            Action move = null;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                move = () => gameController.MoveEast();
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                move = () => gameController.MoveWest();
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                move = () => gameController.MoveNorth();
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                move = () => gameController.MoveSouth();
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                move = gameController.TogglePause;
            }

            if (move != null)
            {
                move();
                animationController.Walk();
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                animationController.Stop();
                yield return null;
            }
        }
    }
}
