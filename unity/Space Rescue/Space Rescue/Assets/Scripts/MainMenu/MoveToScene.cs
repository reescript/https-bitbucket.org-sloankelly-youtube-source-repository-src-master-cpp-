using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{
    bool sceneLoading = false;

    public KeyCode key = KeyCode.Escape;

    public string nextScene;

    void Update()
    {
        if (!sceneLoading && Input.GetKeyUp(key))
        {
            sceneLoading = true;
            SceneManager.LoadScene(nextScene);
        }
    }
}
