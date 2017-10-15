using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public GameController gameController;

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (Input.mousePosition.x < Screen.width / 2)
			{
                gameController.MovePlayerLeft();
			}
			else
			{
                gameController.MovePlayerRight();
			}
		}
	}
}
