using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameController gameController;

    Vector2 mouseStart = Vector2.zero;
    Vector2 mouseEnd = Vector2.zero;

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

        if (Input.GetKeyUp(KeyCode.D))
        {
            gameController.MoveEast();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            gameController.MoveWest();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            gameController.MoveNorth();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            gameController.MoveSouth();
        }
    }
}
