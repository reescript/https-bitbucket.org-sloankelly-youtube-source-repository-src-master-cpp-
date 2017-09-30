using UnityEngine;
using UnityEngine.UI;

public static class TextExtensions
{
    public static void SetAlpha(this Text text, float alpha)
    {
        Color current = text.color;
        current.a = alpha;
        text.color = current;
    }

    public static void SetScale(this Text text, float scale)
    {
        var tmp = new Vector3(scale, scale, scale);
        text.gameObject.transform.localScale = tmp;
    }
}
