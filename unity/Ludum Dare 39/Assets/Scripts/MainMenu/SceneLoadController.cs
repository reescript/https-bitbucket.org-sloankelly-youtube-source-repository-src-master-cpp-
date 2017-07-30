using UnityEngine;

public class SceneLoadController : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public FadeLoadSceneBehaviour fadeLoad;

    internal void ShowInstructions()
    {
        fadeLoad.Load(mainMenu, "Instructions");
    }

    internal void StartGame()
    {
        fadeLoad.Load(mainMenu, "Game");
    }

    internal void Quit()
    {
        fadeLoad.Load(mainMenu, "");
    }
}
