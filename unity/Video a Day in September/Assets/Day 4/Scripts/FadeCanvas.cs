using System.Collections;
using UnityEngine;

public class FadeCanvas : MonoBehaviour 
{

    public CanvasGroup canvasGroup;

    public float duration = 1f;

    public void DoFadeOut()
    {
        StartCoroutine(Fade(CurveFactory.Create(0f, 1f)));
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        DoFadeOut();
    }

    IEnumerator Fade(AnimationCurve curve)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            canvasGroup.alpha = curve.Evaluate(time);

            yield return null;
        }

        canvasGroup.alpha = curve.Evaluate(1f);
    }
}
