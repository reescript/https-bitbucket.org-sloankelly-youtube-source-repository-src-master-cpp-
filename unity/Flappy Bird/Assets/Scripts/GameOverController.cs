using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Image gameOverText;

    public Image placement;

    public Image medal;

    public ScoreAnimator thisSessionsScore;
    public ScoreAnimator bestScore;

    public Button okButton;

    public Button shareButton;

    public Sprite[] medals;
    	
    public void OKButton_Click()
    {
        SceneManager.LoadScene("mainmenu");
    }

	IEnumerator Start ()
    {
        ScoreController score = FindObjectOfType<ScoreController>();
        int currentBestScore = PlayerPrefs.GetInt("bestScore", score.Score);

        if (score.Score >= currentBestScore)
        {
            PlayerPrefs.SetInt("bestScore", score.Score);
        }

        yield return new WaitForSeconds(0.5f);

        Vector3 start = new Vector3(0, 144, 0);
        Vector3 end = new Vector3(0, 54, 0);

        float time = 0;
        while (time < 1f)
        {
            Vector3 pos = Vector3.Lerp(start, end, time);
            time += Time.unscaledDeltaTime;
            gameOverText.transform.position = pos;
            yield return null;
        }

        start = new Vector3(0, -165, 0);
        end = new Vector3(0, 0, 0);

        time = 0;
        while (time < 1f)
        {
            Vector3 pos = Vector3.Lerp(start, end, time);
            time += Time.unscaledDeltaTime;
            placement.transform.position = pos;
            yield return null;
        }

        bestScore.StartAnimation(currentBestScore);
        while (!bestScore.IsComplete)
        {
            yield return null;
        }

        thisSessionsScore.StartAnimation(score.Score);
        while (!thisSessionsScore.IsComplete)
        {
            yield return null;
        }
        
        int medalIndex = 0;

        if (score.Score > 2 && score.Score < 5)
        {
            medalIndex = 1;
        }
        else if (score.Score >= 5 && score.Score < 8)
        {
            medalIndex = 2;
        }
        else if (score.Score > 10)
        {
            medalIndex = 3;
        }
        
        medal.gameObject.SetActive(true);
        bool expand = true;

        float startScale = 0f;
        float endScale = 1f;

        if (medalIndex % 2 == 1)
        {
            startScale = 1f;
            endScale = 0f;
            expand = false;
        }

        for (int i = 0; i <= medalIndex; i++)
        {
            medal.sprite = medals[i];
            time = 0;

            while (time < 1)
            {
                float currentScale = Mathf.Lerp(startScale, endScale, time);
                time += Time.deltaTime / 0.25f;
                medal.transform.localScale = new Vector3(currentScale, 1, 1);
                yield return null;
            }

            expand = !expand;

            if (expand)
            {
                startScale = 0f;
                endScale = 1f;
            }
            else
            {
                startScale = 1f;
                endScale = 0f;
            }
        }

        okButton.gameObject.SetActive(true);
        shareButton.gameObject.SetActive(true);
    }
}
