using System.Collections;
using UnityEngine;

public class ChangeCanvasAlpha : ScriptableBehaviour
{
    public CanvasGroup canvasGroup;

    public float newAlpha;

    public float duration = 1f;

    public override IEnumerator Run()
    {
        float time = 0;

        float existing = canvasGroup.alpha;

        while (time < 1f)
        {
            float current = Mathf.Lerp(existing, newAlpha, time);
            canvasGroup.alpha = current;

            time += Time.deltaTime / duration;

            yield return null;
        }

        var c = Mathf.Lerp(existing, newAlpha, 1f);
        canvasGroup.alpha = c;
    }
}
