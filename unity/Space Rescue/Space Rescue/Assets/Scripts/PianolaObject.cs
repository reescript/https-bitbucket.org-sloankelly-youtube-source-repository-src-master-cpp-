using UnityEngine;
using System.Collections;

public class PianolaObject : MonoBehaviour
{
    private int row;

    private bool killMe;

	public GameController gameController;

	public GameBoard gameBoard;

	public int column;

    public float speed;

    void Update()
    {
        if (killMe)
        {
            Destroy(this);
        }
    }

    IEnumerator Start()
    {
        do
        {
            gameBoard.SetValueAt(column, row);
            yield return new WaitForSeconds(speed);
            gameBoard.SetValueAt(column, row, false);
            row++;

        }
        while (row <= 4);

        gameController.TestCollision(column);
        killMe = true;
    }
}
