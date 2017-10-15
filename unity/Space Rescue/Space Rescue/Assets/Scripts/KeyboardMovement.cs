using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    public GameController gameController;

	void Update()
	{
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            gameController.MovePlayerLeft();
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            gameController.MovePlayerRight();
        }
	}
}
