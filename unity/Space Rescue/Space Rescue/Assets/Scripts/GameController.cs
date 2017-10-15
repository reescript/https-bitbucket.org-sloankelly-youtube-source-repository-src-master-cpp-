using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int rocketColumn = 0;

    bool isRunning = false;

    int numLives = 3;

    public GameBoard gameBoard;

    public AudioClip explosion;

    public AudioClip playerMove;

    public AudioSource audio;

    public Pianola pianola;

    public StartRoundCanvasController startRound;

    public LivesController livesController;

    public CanvasGroup gameOver;

    public float gameStartDelay = 2f;

    public int currentRound = 1;

	public void TestCollision(int column)
	{
        if (column == rocketColumn)
        {
            audio.PlayOneShot(explosion);
            ResetLevel();
        }
	}

    public void MovePlayerLeft()
    {
        if (!isRunning) return;

        gameBoard.SetValueAt(rocketColumn, 5, false);

        rocketColumn--;
        if (rocketColumn < 0)
        {
            rocketColumn = 0;
        }

        gameBoard.SetValueAt(rocketColumn, 5, true);
        audio.PlayOneShot(playerMove);
    }

    public void MovePlayerRight()
    {
        if (!isRunning) return;

        gameBoard.SetValueAt(rocketColumn, 5, false);

        rocketColumn++;
        if (rocketColumn > 4)
        {
            rocketColumn = 4;
        }
        gameBoard.SetValueAt(rocketColumn, 5, true);
        audio.PlayOneShot(playerMove);
    }

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(StartTheGame());
    }

    IEnumerator StartTheGame()
    {
        rocketColumn = 0;

        startRound.SetWaveNumber(currentRound);
        startRound.ToggleVisible();

        livesController.UpdateLives(numLives);
        
        gameBoard.Clear();
        gameBoard.SetValueAt(rocketColumn, 5, true);

        yield return new WaitForSeconds(gameStartDelay);

        startRound.ToggleVisible(false);

        isRunning = true;
        pianola.StartPianola();
    }

    IEnumerator RestartTheGame()
    {
        // TODO: Put something a little better in here
        // Play a tune or whatever
        yield return new WaitForSeconds(2);

        yield return StartTheGame();
    }

    IEnumerator GameOver()
    {
		// TODO: GAME OVER MAN, GAME OVER! -- Bill Paxton
		gameBoard.Clear();
		gameOver.alpha = 1f;
		yield return new WaitForSeconds(4);
		// TODO: Load game
	}

    void ResetLevel()
    {
        isRunning = false;
        numLives--;

        pianola.Reset();

        if (numLives == -1)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            StartCoroutine(RestartTheGame());
        }
    }
}
