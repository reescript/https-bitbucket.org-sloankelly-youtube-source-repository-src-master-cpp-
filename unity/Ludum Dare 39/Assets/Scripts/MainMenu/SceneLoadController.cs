using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadController : MonoBehaviour
{
    public CanvasGroup mainMenu;
    
    internal void ShowInstructions()
    {
        StartCoroutine(LoadScene("Instructions"));
    }

    internal void StartGame()
    {
        StartCoroutine(LoadScene("Game"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        float alpha = mainMenu.alpha;
        while (alpha > 0)
        {
            mainMenu.alpha = alpha;
            alpha -= Time.deltaTime;

            yield return null;
        }

        mainMenu.alpha = 0f;

        if (string.IsNullOrEmpty(sceneName))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    internal void Quit()
    {
        StartCoroutine(LoadScene(""));
    }
}
