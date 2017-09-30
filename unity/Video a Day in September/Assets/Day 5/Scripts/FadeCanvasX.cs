using System;
using System.Collections;
using UnityEngine;

public class FadeCanvasX : MonoBehaviour 
{
    public CanvasGroup canvasGroup;

    public GameObject obj;

    public float duration = 1f;

    public void DoFadeOut()
    {
        var curve = CurveFactory.Create(1f, 0f);
        Action<float> fadeOut = (t) => canvasGroup.alpha = curve.Evaluate(t);
        Action<float> scaleCanvas = (t) => obj.transform.localScale = new Vector3(curve.Evaluate(t), curve.Evaluate(t), curve.Evaluate(t));
        Action<float> combined = fadeOut + scaleCanvas;
        StartCoroutine(CoroutineFactory.Create(duration, combined));
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        DoFadeOut();
    }
}
