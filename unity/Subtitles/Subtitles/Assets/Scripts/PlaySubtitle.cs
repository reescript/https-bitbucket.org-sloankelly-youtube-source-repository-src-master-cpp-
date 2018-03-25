using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitle : MonoBehaviour
{
    private AudioSource audioSource;
    private ScriptManager scriptManager;
    private SubtitleGuiManager guiManager;

    private void Awake()
    {
        scriptManager = FindObjectOfType<ScriptManager>();
        guiManager = FindObjectOfType<SubtitleGuiManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DoSubtitle());
        }
    }

    private IEnumerator DoSubtitle()
    {
        var script = scriptManager.GetText(audioSource.clip.name);
        var lineDuration = audioSource.clip.length / script.Length;

        foreach (var line in script)
        {
            guiManager.SetText(line);
            yield return new WaitForSeconds(lineDuration);
        }
        
        guiManager.SetText(string.Empty);
    }
}
