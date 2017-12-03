using System;
using System.Collections;
using UnityEngine;

public class SpiralFade : MonoBehaviour
{
    public Action endAction = () => { };

	// Use this for initialization
	IEnumerator Start ()
    {
        yield return null;

        Vector3 targetScale = gameObject.transform.localScale * 3;

        float time = 0f;

        while(time < 1f)
        {
            var s = Vector3.Lerp(gameObject.transform.localScale, targetScale, time);
            gameObject.transform.localScale = s;

            time += Time.deltaTime / 0.5f;
            yield return null;
        }

        time = 0f;
        while (time < 1f)
        {
            var s = Vector3.Lerp(gameObject.transform.localScale, Vector3.zero, time);
            gameObject.transform.localScale = s;

            gameObject.transform.Rotate(Vector3.forward, 30 * Time.deltaTime);

            time += Time.deltaTime / 0.25f;
            yield return null;
        }

        endAction();
    }
}
