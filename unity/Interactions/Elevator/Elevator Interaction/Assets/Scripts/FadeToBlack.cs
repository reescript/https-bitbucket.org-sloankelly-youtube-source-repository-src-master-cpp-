using SloanKelly.GameLib;
using UnityEngine;

/// <summary>
/// Requires the CoroutineFactor.Create() method in Sloan Kelly Game Lib.
/// </summary>
public class FadeToBlack : MonoBehaviour
{
    [Tooltip("The target canvas group that provides the full-screen image of the colour you want to fade out to")]
    public CanvasGroup targetCanvasGroup;

    [Tooltip("The duration of the fade")]
    public float duration;

    [Tooltip("Set to true if the fade should happen on a volume trigger")]
    public bool fadeOnTrigger = true;

    [Tooltip("If the fade is on a trigger, set the object tag that can trigger the event")]
    public string triggerFilter = "Player";

    /// <summary>
    /// Do the fade out. Can be called manually or if fadeOnTrigger is true when the trigger is hit.
    /// </summary>
    public void DoFade()
    {
        StartCoroutine(CoroutineFactory.Create(duration, time =>
        {
            targetCanvasGroup.alpha = time;
        }));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!fadeOnTrigger) return;

        if (!string.IsNullOrEmpty(triggerFilter) && !other.tag.Equals(triggerFilter))
        {
            return;
        }

        DoFade();
    }
}
