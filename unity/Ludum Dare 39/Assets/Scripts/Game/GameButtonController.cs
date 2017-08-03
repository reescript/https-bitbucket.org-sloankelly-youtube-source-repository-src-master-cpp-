using System;
using UnityEngine.SceneManagement;

public class GameButtonController : PropertyChangedBehaviour
{
    Action returnToMainMenu;
    Action playAgain;
    Action resumeGameCommand;

    public GameController gameController;

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

    public Action ResumeGameCommand
    {
        get { return resumeGameCommand; }
        set { resumeGameCommand = value; OnPropertyChanged("ResumeGameCommand"); }
    }

    void Start()
    {
        ReturnToMainMenuCommand = () => SceneManager.LoadScene("MainMenu");
        PlayAgainCommand = () => SceneManager.LoadScene("Game");
        ResumeGameCommand = gameController.ResumeGame;
    }
}
