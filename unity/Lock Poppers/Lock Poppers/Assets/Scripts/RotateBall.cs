﻿using System;
using System.Collections;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    float duration = 1f;

    public AnimationCurve scaleIn;

    public AnimationCurve scaleOut;

    public AnimationCurve opacityIn;

    public AnimationCurve opacityOut;

    public void StartFadeIn(Action finishedFade, float duration, float direction)
    {
        StartCoroutine(DoFade(finishedFade, scaleIn, opacityIn, duration, direction, true));
    }

    public void StartFadeOut(Action finishedFade, float duration, float direction)
    {
        StartCoroutine(DoFade(finishedFade, scaleOut, opacityOut, duration, direction));
    }

    IEnumerator DoFade(Action finishedFade, AnimationCurve scaleCurve, AnimationCurve opacityCurve, float duration, float direction, bool moveBall = false)
    {
        if (moveBall)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90 + UnityEngine.Random.Range(0, 180) * direction);
        }

        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        var ballTransform = spriteRenderer.transform;

        float time = 0f;
        while (time < 1f)
        {
            float currentScale = scaleCurve.Evaluate(time);
            float currentOpacity = opacityCurve.Evaluate(time);

            ballTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            spriteRenderer.color = new Color(1, 1, 1, currentOpacity);

            time += Time.deltaTime / duration;
            yield return null;
        }

        if (finishedFade != null)
        {
            finishedFade();
        }
    }
}
