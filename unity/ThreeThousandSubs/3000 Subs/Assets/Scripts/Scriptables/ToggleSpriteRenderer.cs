using UnityEngine;
using System.Collections;

public class ToggleSpriteRenderer : ScriptableBehaviour
{
    public SpriteRenderer[] sprites;
    
    public bool spriteEnabled = false;

    public override IEnumerator Run()
    {
        foreach (var sprite in sprites)
        {
            sprite.enabled = spriteEnabled;
        }

        yield return null;
    }
}
