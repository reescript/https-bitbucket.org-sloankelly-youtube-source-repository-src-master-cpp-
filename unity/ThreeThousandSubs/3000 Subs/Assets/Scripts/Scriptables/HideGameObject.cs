using System.Collections;
using UnityEngine;

public class HideGameObject : ScriptableBehaviour
{
    public GameObject target;

    public bool showObject;

    public override IEnumerator Run()
    {
        target.SetActive(showObject);
        yield return null;
    }
}
