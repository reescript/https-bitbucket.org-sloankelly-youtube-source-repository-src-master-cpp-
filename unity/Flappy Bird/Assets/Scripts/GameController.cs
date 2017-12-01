using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    bool gamePaused = false;
    bool gameIsOver = false;

    Pipework groundCollision;

    public MoveGround ground;

    public PipeSpawner pipeSpawner;

    public GameObject flappy;

    public ScoreController score;

    public GameOverController gameOverScreen;

    public Canvas playCanvas;

    public float force = 10f;

    public int currentSpeed = 35;

    public Sprite paused;

    public Sprite play;

    public void PauseButton_Clicked(Image pauseButton)
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            pauseButton.sprite = play;
        }
        else
        {
            pauseButton.sprite = paused;
        }

        TogglePause(gamePaused);
    }

    public void PipeHasBeenPassed()
    {
        score.Score++;
    }

    void Start()
    {
        groundCollision = ground.GetComponent<Pipework>();

        ChangeSpeed(currentSpeed);
    }

    void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var rb = flappy.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        if (pipeSpawner.hit || groundCollision.hit)
        {
            // Stop the game, play game over etc.
            TogglePause(true);

            // Turn off the play canvas
            playCanvas.enabled = false;

            // TODO: Enable the game over canvas
            gameIsOver = true;
            gameOverScreen.gameObject.SetActive(true);
            flappy.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    void ChangeSpeed(float speed)
    {
        ground.speed = speed;
        pipeSpawner.speed = speed;
    }

    void TogglePause(bool gameIsPaused)
    {
        if (gameIsPaused)
        {
            ChangeSpeed(0);
            Time.timeScale = 0;
        }
        else
        {
            ChangeSpeed(currentSpeed);
            Time.timeScale = 1;
        }
    }
}
