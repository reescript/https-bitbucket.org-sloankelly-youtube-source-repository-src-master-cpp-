using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ControlVolumeTextVersion : MonoBehaviour
{
    private bool volumeOn;

    public string[] images;

    public AudioMixer audioMix;

    public Text targetText;

    void Awake()
    {
        volumeOn = PlayerPrefs.GetInt("volumeOn", 1) == 1;
        Toggle(volumeOn);
    }

    void Start()
    {
        var button = GetComponent<Button>();

        UnityAction actionWrapper = new UnityAction(Toggle_Volume);
        button.onClick.AddListener(actionWrapper);
    }

    void Toggle_Volume()
    {
        Toggle(!volumeOn);
    }

    void Toggle(bool volOn)
    {
        volumeOn = volOn;

        PlayerPrefs.SetInt("volumeOn", volumeOn ? 1 : 0);
        audioMix.SetFloat("FxVolume", volumeOn ? 0f : -80f);

        targetText.text = images[volumeOn ? 1 : 0];
    }
}
