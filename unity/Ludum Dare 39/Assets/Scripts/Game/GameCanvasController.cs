using System.Collections;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    public CanvasGroup inGameUI;
    public CanvasGroup oxygenDeath;
    public CanvasGroup noRescueDeath;
    public CanvasGroup rescued;

    public void DoOxygenDeath()
    {
        StartCoroutine(SwapCanvas(inGameUI, oxygenDeath, 1f, true));
    }
    
    internal void DoNoRescue()
    {
        StartCoroutine(SwapCanvas(inGameUI, noRescueDeath, 1f, true));
    }

    internal void DoRescued()
    {
        StartCoroutine(SwapCanvas(inGameUI, rescued, 1f, true));
    }

    private IEnumerator SwapCanvas(CanvasGroup first, CanvasGroup second, float duration, bool disable)
    {
        float time = 0f;
        float alpha = 0f;

        while (time < 1f)
        {
            alpha += Time.deltaTime / duration;

            if (alpha > 1f)
            {
                alpha = 1f;
            }

            first.alpha = 1f - alpha;
            second.alpha = alpha;

            yield return null;
        }

        first.alpha = 0f;
        second.alpha = 1f;

        if (disable)
        {
            first.gameObject.SetActive(false);
        }
    }
}
