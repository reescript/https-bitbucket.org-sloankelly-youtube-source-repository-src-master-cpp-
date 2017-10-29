using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartTheGame : MonoBehaviour
{
    void Awake()
    {
        var button = GetComponent<Button>();

        Action launchGame = () =>
        {
            PlayerPrefs.SetInt("currentRound", 1);
            SceneManager.LoadScene("Game");
        };

        UnityAction actionWrapper = new UnityAction(launchGame);
        button.onClick.AddListener(actionWrapper);
    }
}
