using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    enum GameState
    {
        Playing,
        Interstitial
    }

    private const int CurrentLevel = 1; // For debug purposes only! Should be 1 for shipping game

    private const float BallFadeInOutTime = 0.25f;

    public RotateBall ballPivot;

    public RotateTick tickPivot;

    public LockHoopAnimation lockHoop;

    public Text levelText;

    public Text dialText;

    float direction = 1f;

    int currentLevel = CurrentLevel;

    GameState state = GameState.Interstitial;

    int tapsLeft = CurrentLevel;

    public float tickSpeed = 60f;

    private void Awake()
    {
        tickPivot.missedTheBall = MissedTheBall;
        UpdateUI();
    }
    
    private void ShowTick()
    {
        tickPivot.angleSpeed = tickSpeed;
        tickPivot.Rotate(true);
        tickPivot.gameObject.SetActive(true);
    }

    private void MissedTheBall()
    {
        if (state == GameState.Interstitial)
        {
            return;
        }

        // TODO: HANDLE GAME OVER
    }

    private void OnMouseDown()
    {
        // TODO: ARE WE ACTUALLY PLAYING THE GAME!??!

        switch (state)
        {
            case GameState.Interstitial:
                // TODO: Game set up stuff
                ballPivot.StartFadeIn(ShowTick, BallFadeInOutTime, 1f);
                state = GameState.Playing;
                break;
            default:
                if (tickPivot.InsideBall)
                {
                    tapsLeft--;
                    if (tapsLeft == 0)
                    {
                        tickPivot.Rotate(false);
                        ballPivot.StartFadeOut(DoCelebration, BallFadeInOutTime, 1f);
                    }
                    else
                    {
                        direction *= -1;
                        tickPivot.direction = direction;
                        ballPivot.StartFadeOut(Ball_FadedOut, BallFadeInOutTime, 1f);
                        UpdateUI();
                    }
                }
                break;
        }
    }

    private void DoCelebration()
    {
        tickPivot.gameObject.SetActive(false);
        lockHoop.ShowUnlock(Unlock_Finished, 1f);
    }

    private void Unlock_Finished()
    {
        // TODO: SHOW END SCREEN BIT HERE
        state = GameState.Interstitial;
        tickPivot.Reset();
        lockHoop.Reset();
        tickPivot.Reset();
        currentLevel++;
        tapsLeft = currentLevel;
        UpdateUI();
    }

    private void Ball_FadedOut()
    {
        tickPivot.Reset();
        ballPivot.StartFadeIn(ShowTick, BallFadeInOutTime, 1f);
    }
    
    private void UpdateUI()
    {
        levelText.text = "Level: " + currentLevel;
        dialText.text = tapsLeft.ToString();
    }
}
