using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StartRoundCanvasController : MonoBehaviour 
{
    private CanvasGroup group;

    public Text waveText;

    public void SetWaveNumber(int waveNumber)
    {
        waveText.text = waveNumber.ToString();
    }

    public void ToggleVisible(bool visible = true)
    {
        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
    }

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }
}
