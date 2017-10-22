using System.Collections;
using UnityEngine;

public class WaitFor : ScriptableBehaviour
{
    public float duration = 1f;

    public override IEnumerator Run()
    {
        yield return new WaitForSeconds(duration);
    }
}
