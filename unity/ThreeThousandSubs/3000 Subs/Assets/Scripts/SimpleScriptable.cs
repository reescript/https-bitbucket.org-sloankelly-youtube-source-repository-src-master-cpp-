using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleScriptable : MonoBehaviour
{
    IEnumerable<ScriptableBehaviour> scriptables;

    [Tooltip("The scriptables will execute when the object start")]
    public bool runOnStartup = false;

    public IEnumerator RunScriptables()
    {
        foreach (var scriptable in scriptables)
        {
            yield return scriptable.Run();
        }
    }

    void Awake()
    {
        scriptables = GetComponents<ScriptableBehaviour>();
    }

    IEnumerator Start()
    {
        if (!runOnStartup)
        {
            yield break;
        }
        else
        {
            yield return RunScriptables();
        }
    }
}
