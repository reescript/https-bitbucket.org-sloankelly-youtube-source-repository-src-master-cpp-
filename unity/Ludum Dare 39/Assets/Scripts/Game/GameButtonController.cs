using System;
using UnityEngine.SceneManagement;

public class GameButtonController : PropertyChangedBehaviour
{
    Action returnToMainMenu;
    Action playAgain;

    public Action ReturnToMainMenuCommand
    {
        get { return returnToMainMenu; }
        set { returnToMainMenu = value; OnPropertyChanged("ReturnToMainMenuCommand"); }
    }

    public Action PlayAgainCommand
    {
        get { return playAgain; }
        set { playAgain = value; OnPropertyChanged("PlayAgainCommand"); }
    }

    void Start()
    {
        ReturnToMainMenuCommand = () => SceneManager.LoadScene("MainMenu");
        PlayAgainCommand = () => SceneManager.LoadScene("Game");
    }
}
