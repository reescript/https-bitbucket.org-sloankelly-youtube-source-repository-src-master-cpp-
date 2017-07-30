using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeLoadSceneBehaviour : MonoBehaviour
{
    public void Load(CanvasGroup canvasGroup, string sceneName)
    {
        StartCoroutine(LoadScene(canvasGroup, sceneName));
    }

    IEnumerator LoadScene(CanvasGroup canvasGroup, string sceneName)
    {
        float alpha = canvasGroup.alpha;
        while (alpha > 0)
        {
            canvasGroup.alpha = alpha;
            alpha -= Time.deltaTime;

            yield return null;
        }

        canvasGroup.alpha = 0f;

        if (string.IsNullOrEmpty(sceneName))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
