using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score;

    public GameObject[] indicators;

    public Sprite[] numbers;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            UpdateVisual();
        }
    }

    void Start()
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        string scr = score.ToString();

        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].SetActive(false);
        }

        for (int i = scr.Length - 1; i >=0; i--)
        {
            int index = (scr.Length - 1) - i;
            int spriteIndex = scr[i] - '0';

            indicators[i].GetComponent<Image>().sprite = numbers[spriteIndex];
            indicators[i].SetActive(true);
        }
    }
}
