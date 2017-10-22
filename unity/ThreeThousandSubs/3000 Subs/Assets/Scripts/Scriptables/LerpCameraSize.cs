using UnityEngine;
using System.Collections;

public class LerpCameraSize : ScriptableBehaviour
{
    public Camera cam;

    public float newCameraSize;

    public float duration = 1f;

    public override IEnumerator Run()
    {
        float time = 0;

        float existing = cam.orthographicSize;

        while (time < 1f)
        {
            float current = Mathf.Lerp(existing, newCameraSize, time);
            cam.orthographicSize = current;

            time += Time.deltaTime / duration;

            yield return null;
        }

        var c = Mathf.Lerp(existing, newCameraSize, 1f);
        cam.orthographicSize = c;
    }
}
