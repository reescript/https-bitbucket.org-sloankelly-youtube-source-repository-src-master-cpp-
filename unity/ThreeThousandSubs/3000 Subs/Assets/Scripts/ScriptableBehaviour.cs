using UnityEngine;
using System.Collections;

public abstract class ScriptableBehaviour : MonoBehaviour
{
    /// <summary>
    /// This method MUST be implemented in child classes for this type.
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Run();
}
