using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickToScene : MonoBehaviour
{
    public string nextScene;

    void Awake()
    {
        var button = GetComponent<Button>();

        Action launchGame = () =>
        {
            SceneManager.LoadScene(nextScene);
        };

        UnityAction actionWrapper = new UnityAction(launchGame);
        button.onClick.AddListener(actionWrapper);
    }
}
