using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAnimator : MonoBehaviour
{
    public Sprite[] numbers;

    public Image[] indicators;

    public bool IsComplete { get; private set; }

    public void StartAnimation(int score)
    {
        StartCoroutine(AnimateScore(score));
    }

    IEnumerator AnimateScore(int score)
    {
        for (int i = 0; i <= score; i++)
        {
            UpdateVisual(i);
            yield return new WaitForSeconds(0.1f);
        }

        IsComplete = true;
    }
    
    void UpdateVisual(int score)
    {
        string scr = score.ToString();

        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].gameObject.SetActive(false);
        }

        for (int i = scr.Length - 1; i >= 0; i--)
        {
            int index = (scr.Length - 1) - i;
            int spriteIndex = scr[i] - '0';

            indicators[i].GetComponent<Image>().sprite = numbers[spriteIndex];
            indicators[i].gameObject.SetActive(true);
        }
    }
}
