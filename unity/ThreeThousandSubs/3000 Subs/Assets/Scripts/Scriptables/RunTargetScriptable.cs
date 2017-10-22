using System.Collections;

public class RunTargetScriptable : ScriptableBehaviour
{
    public SimpleScriptable runnableTarget;

    public bool immediateReturn = false;

    public override IEnumerator Run()
    {
        if (immediateReturn)
        {
            runnableTarget.RunScriptables();
            yield return null;
        }
        else
        {
            yield return runnableTarget.RunScriptables();
        }
    }
}
