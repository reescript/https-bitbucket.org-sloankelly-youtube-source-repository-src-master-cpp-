using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public RotateBall ballPivot;

    public RotateTick tickPivot;

    public Text levelText;

    public Text dialText;

    float direction = 1f;

    int currentLevel = 10;

    int tapsLeft = 10;

    public float tickSpeed = 60f;

    private void Awake()
    {
        tickPivot.missedTheBall = MissedTheBall;

        levelText.text = "Level: " + currentLevel;
        dialText.text = tapsLeft.ToString();
    }

    private void Start()
    {
        ballPivot.StartFadeIn(ShowTick, 0.5f, 1f);
    }
    
    private void ShowTick()
    {
        tickPivot.angleSpeed = tickSpeed;
        tickPivot.gameObject.SetActive(true);
    }

    private void MissedTheBall()
    {
        // TODO: GAME OVER
    }

    private void OnMouseDown()
    {
        // TODO: ARE WE ACTUALLY PLAYING THE GAME!??!

        if (tickPivot.InsideBall)
        {
            tapsLeft--;
            if (tapsLeft == 0)
            {
                DoCelebration();
            }
            else
            {
                direction *= -1;
                tickPivot.direction = direction;
                ballPivot.StartFadeOut(Ball_FadedOut, 0.5f, 1f);
                dialText.text = tapsLeft.ToString();
            }
        }
    }

    private void DoCelebration()
    {

    }

    private void Ball_FadedOut()
    {
        tickPivot.Reset();
        ballPivot.StartFadeIn(ShowTick, 0.5f, 1f);
    }
}
