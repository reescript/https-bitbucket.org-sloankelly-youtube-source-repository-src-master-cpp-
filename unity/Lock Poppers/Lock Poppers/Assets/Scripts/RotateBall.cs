using System;
using System.Collections;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    float duration = 1f;

    public void StartRotation(Action finishedRotation, float direction = 1)
    {
        StartCoroutine(RotateMe(finishedRotation, direction));
    }

    IEnumerator RotateMe(Action finishedRotation, float direction)
    {
        Quaternion current = transform.rotation;

        Quaternion target = Quaternion.Euler(0, 0, 90 + UnityEngine.Random.Range(0, 180) * direction);

        float time = 0f;

        while (time < 1f)
        {
            transform.rotation = Quaternion.Lerp(current, target, time);
            time += Time.deltaTime / duration;

            yield return null;
        }

        if (finishedRotation != null)
        {
            finishedRotation();
        }
    }
}
