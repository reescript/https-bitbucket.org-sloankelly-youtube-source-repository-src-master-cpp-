using UnityEngine;
using System.Collections;

public class MoveFromTo : ScriptableBehaviour
{
    public Transform start;

    public Transform end;

    public float duration = 1f;

    public override IEnumerator Run()
    {
        float time = 0f;

        Vector3 sp = start.position;
        Vector3 ep = end.position;

        while (time < 1f)
        {
            Vector3 currentPosition = Vector3.Lerp(sp, ep, time);
            transform.position = currentPosition;

            time += Time.deltaTime / duration;
            yield return null;
        }
        
        var cp = Vector3.Lerp(sp, ep, 1f);
        transform.position = cp;
    }
}
