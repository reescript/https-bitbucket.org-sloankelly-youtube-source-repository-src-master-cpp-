using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SaveGameController))]
public class GameController : MonoBehaviour
{
    enum GameState
    {
        Playing,
        Interstitial
    }

    private const float BallFadeInOutTime = 0.25f;

    private SaveGameController saveGameController;

    public Transform lockBody;

    public RotateBall ballPivot;

    public RotateTick tickPivot;

    public LockHoopAnimation lockHoop;

    public Text levelText;

    public Text dialText;

    float direction = 1f;

    int currentLevel = 1;

    GameState state = GameState.Interstitial;

    int tapsLeft = 1;

    public float tickSpeed = 60f;

    IEnumerator Start()
    {
        saveGameController = GetComponent<SaveGameController>();

        while (!saveGameController.IsReady)
            yield return null;

        currentLevel = saveGameController.CurrentLevel;
        tapsLeft = saveGameController.CurrentLevel;

        tickPivot.missedTheBall = MissedTheBall;
        UpdateUI();
    }
    
    private void ShowTick()
    {
        tickPivot.angleSpeed = tickSpeed;
        tickPivot.Rotate(true);
        tickPivot.gameObject.SetActive(true);
        state = GameState.Playing;
    }

    private void MissedTheBall()
    {
        if (state == GameState.Interstitial)
        {
            return;
        }

        StartCoroutine(ShakeTheLock());
    }

    private void OnApplicationFocus (bool focused)
    {
        if (!focused)
        {
            state = GameState.Interstitial;
            tickPivot.Rotate(false);
            tickPivot.gameObject.SetActive(false);
            lockBody.localPosition = Vector3.zero;
            Unlock_Finished(currentLevel);
        }
    }

    private void OnMouseDown()
    {
        // TODO: ARE WE ACTUALLY PLAYING THE GAME!??!

        switch (state)
        {
            case GameState.Interstitial:
                // TODO: Game set up stuff
                ballPivot.StartFadeIn(ShowTick, BallFadeInOutTime, 1f, tickPivot.ZRotation);
                state = GameState.Playing;
                break;
            default:
                if (tickPivot.InsideBall)
                {
                    tapsLeft--;
                    if (tapsLeft == 0)
                    {
                        tickPivot.Rotate(false);
                        ballPivot.StartFadeOut(DoCelebration, BallFadeInOutTime, 1f, tickPivot.ZRotation);
                    }
                    else
                    {
                        state = GameState.Interstitial;
                        direction *= -1;
                        tickPivot.direction = direction;
                        ballPivot.StartFadeOut(Ball_FadedOut, BallFadeInOutTime, 1f, tickPivot.ZRotation);
                        UpdateUI();
                    }
                }
                else
                {
                    StartCoroutine(ShakeTheLock());
                }
                break;
        }
    }

    private void DoCelebration()
    {
        state = GameState.Interstitial;
        tickPivot.gameObject.SetActive(false);
        var newLevel = currentLevel++;
        saveGameController.SaveProgress(newLevel + 1);

        lockHoop.ShowUnlock(() => Unlock_Finished(newLevel), 1f);
    }

    private void Unlock_Finished(int newLevel)
    {
        // TODO: SHOW END SCREEN BIT HERE
        tickPivot.Reset();
        lockHoop.Reset();
        tickPivot.Reset();
        tapsLeft = currentLevel;
        UpdateUI();
    }

    private void Ball_FadedOut()
    {
        tickPivot.Reset();
        ballPivot.StartFadeIn(ShowTick, BallFadeInOutTime, 1f, tickPivot.ZRotation);
    }
    
    private void UpdateUI()
    {
        levelText.text = "Level: " + currentLevel;
        dialText.text = tapsLeft.ToString();
    }

    IEnumerator ShakeTheLock()
    {
        const float duration = 0.5f;

        state = GameState.Interstitial;
        tickPivot.Rotate(false);
        tickPivot.gameObject.SetActive(false);

        float time = 0f;
        while (time < 1f)
        {
            float newx = -0.25f + Random.Range(0f, 0.5f);
            lockBody.localPosition = new Vector3(newx, 0f, 0f);

            time += Time.deltaTime / duration;

            yield return null;
        }

        lockBody.localPosition = Vector3.zero;
        Unlock_Finished(currentLevel);
    }
}
