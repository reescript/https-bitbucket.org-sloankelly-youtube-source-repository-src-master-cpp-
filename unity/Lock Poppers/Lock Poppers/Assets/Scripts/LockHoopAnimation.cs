using System;
using System.Collections;
using UnityEngine;

public class LockHoopAnimation : MonoBehaviour
{
    public AnimationCurve heightCurve;

    public Transform child;

    public void Reset()
    {
        child.localPosition = Vector3.zero;
    }

    public void ShowUnlock(Action finished, float duration)
    {
        StartCoroutine(DoUnlock(finished, duration));
    }

    IEnumerator DoUnlock(Action finished, float duration)
    {
        float time = 0f;
        while (time < 1f)
        {
            float currentHeight = heightCurve.Evaluate(time);

            child.localPosition = new Vector3(0, currentHeight, 0);

            time += Time.deltaTime / duration;
            yield return null;
        }

        if (finished != null)
        {
            finished();
        }
    }
}
