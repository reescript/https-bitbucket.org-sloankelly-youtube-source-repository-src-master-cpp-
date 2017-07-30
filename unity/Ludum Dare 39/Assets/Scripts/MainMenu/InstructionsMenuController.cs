using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenuController : MonoBehaviour
{
    int index = 0;

    public GameObject[] panels;

    public CanvasGroup canvas;

    public FadeLoadSceneBehaviour fadeLoad;


    public void Back()
    {
        index--;
        if (index < 0)
        {
            index = panels.Length - 1;
        }

        for (int i = 0; i < panels.Length; i++) panels[i].SetActive(i == index);
    }

    public void Forward()
    {
        index++;
        if (index == panels.Length)
        {
            index = 0;
        }

        for (int i = 0; i < panels.Length; i++) panels[i].SetActive(i == index);
    }

    public void MainMenu()
    {
        fadeLoad.Load(canvas, "MainMenu");
    }
}
