using System.Collections;
using UnityEngine;

public class FadeUpController : MonoBehaviour
{
    public CanvasGroup canvas;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        float alpha = 0f;
        canvas.alpha = alpha;

        while (alpha < 1f)
        {
            canvas.alpha = alpha;
            alpha += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = 1f;
    }
}
