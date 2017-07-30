﻿using System;
using UnityEngine;

public class MainMenuButtonController : PropertyChangedBehaviour
{
    Action playGame;
    Action instructions;
    Action quitToOs;

    public SceneLoadController sceneLoader;

    public Action PlayGame
    {
        get { return playGame; }
        set { playGame = value; OnPropertyChanged("PlayGame"); }
    }

    public Action Instructions
    {
        get { return instructions; }
        set { instructions = value; OnPropertyChanged("Instructions"); }
    }

    public Action QuitToOs
    {
        get { return quitToOs; }
        set { quitToOs = value; OnPropertyChanged("QuitToOs"); }
    }

    void Start()
    {
        PlayGame = sceneLoader.StartGame;
        Instructions = sceneLoader.ShowInstructions;
        QuitToOs = sceneLoader.Quit;
    }
}
