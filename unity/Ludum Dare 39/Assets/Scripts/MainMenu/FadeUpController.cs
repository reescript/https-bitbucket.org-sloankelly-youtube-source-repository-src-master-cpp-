using System.Collections;
using UnityEngine;

public class FadeUpController : MonoBehaviour
{
    public CanvasGroup canvas;

    public bool goFromBlack = false;
    public bool disableAfterFade = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        AnimationCurve curve = new AnimationCurve();

        if (goFromBlack)
        {
            curve.AddKey(0f, 1f);
            curve.AddKey(1f, 0f);
        }
        else
        {
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }

        float time = 0f;
        while (time < 1f)
        {
            canvas.alpha = curve.Evaluate(time);
            time += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = curve.Evaluate(1f);

        if (disableAfterFade)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
