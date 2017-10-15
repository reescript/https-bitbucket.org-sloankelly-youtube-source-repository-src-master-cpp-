using UnityEngine;
using System.Collections;

public static class PianolaObjectFactory
{
    public static PianolaObject Create(GameObject go, int column, float speed, GameBoard gameBoard,
                                      GameController gameController)
    {
        var obj = go.AddComponent<PianolaObject>();
        obj.column = column;
        obj.speed = speed;
        obj.gameBoard = gameBoard;
        obj.gameController = gameController;
        return obj;
    }
}
