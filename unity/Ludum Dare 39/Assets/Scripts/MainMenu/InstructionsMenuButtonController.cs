using System;

public class InstructionsMenuButtonController : PropertyChangedBehaviour
{
    Action back;
    Action forward;
    Action mainMenu;

    public InstructionsMenuController controller;

    public Action Back
    {
        get { return back; }
        set { back = value; OnPropertyChanged("Back"); }
    }

    public Action Forward
    {
        get { return forward; }
        set { forward = value; OnPropertyChanged("Forward"); }
    }

    public Action MainMenu
    {
        get { return mainMenu; }
        set { mainMenu = value; OnPropertyChanged("MainMenu"); }
    }

    void Start()
    {
        Back = controller.Back;
        Forward = controller.Forward;
        MainMenu = controller.MainMenu;
    }
}

