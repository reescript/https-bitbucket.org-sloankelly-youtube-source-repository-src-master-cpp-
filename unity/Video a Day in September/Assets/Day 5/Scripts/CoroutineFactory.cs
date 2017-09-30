using System;
using System.Collections;
using UnityEngine;

public static class CoroutineFactory
{
    public static IEnumerator Create(float duration, Action<float> action, Action postCondition = null)
    {
        float time = 0f;


        while (time < 1f) 
        {
            time += Time.deltaTime / duration;
            action(time);

            yield return null;
        }

        action(1f);

		if (postCondition != null)
		{
			postCondition();
		}
    }
}
