using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LaunchUrl : MonoBehaviour
{
    public string url = "https://youtube.com/sloankelly/";

    void Awake()
    {
        var button = GetComponent<Button>();

        Action launchUrl = () =>
        {
            Application.OpenURL(url);
        };

        UnityAction actionWrapper = new UnityAction(launchUrl);
        button.onClick.AddListener(actionWrapper);
    }
}
