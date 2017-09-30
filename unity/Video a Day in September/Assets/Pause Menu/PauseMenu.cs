using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour 
{
    private bool isFading = false;
    private bool isShowing = false;
    private Canvas canvas;
    private CanvasGroup group;

    public float duration = 1f;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        group = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (isFading) return; // early-out if there is a fade going on

        if (!isShowing && Input.GetKeyUp(KeyCode.Escape))
        {
            StartCoroutine(FadeFromTo(0f, 1f, true));
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            StartCoroutine(FadeFromTo(1f, 0f, false));
        }
    }

    IEnumerator FadeFromTo(float from, float to, bool showing)
    {
        isFading = true;

        var curve = new AnimationCurve(new Keyframe[] {
            new Keyframe(0f, from),
            new Keyframe(1f, to)
        });

        float time = 0f;

        while (time < 1f)
        {
            group.alpha = curve.Evaluate(time);
            time += Time.deltaTime;

            yield return null;
        }

        // Ensure that alpha is set to the t=1 value
        group.alpha = curve.Evaluate(1f);

        isFading = false;
        isShowing = showing;
    }

}
