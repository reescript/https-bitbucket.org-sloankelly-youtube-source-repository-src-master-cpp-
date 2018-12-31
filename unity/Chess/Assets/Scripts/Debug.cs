using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour
{
    ChessSerializer serializer;
    Board currentGame;

    void Awake()
    {
        serializer = new ChessSerializer();
    }

    public void Button_Create()
    {
        currentGame = serializer.CreateNewGame();
        serializer.SaveGame(currentGame);
    }

    public void Button_LoadGame()
    {
        currentGame = serializer.LoadGame();
        GetComponent<BoardRenderer>().Initialize(currentGame);
    }

    public void Button_LoadGameBinary()
    {
        currentGame = serializer.LoadGameBinary();
        GetComponent<BoardRenderer>().Initialize(currentGame);
    }

    public void Button_SaveBinary()
    {
        serializer.SaveGameBinary(currentGame);
    }
}
