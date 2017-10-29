using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int rocketColumn = 0;

    bool isRunning = false;

    int numLives = 3;

    public GameBoard gameBoard;

    public AudioClip explosion;

    public AudioClip playerMove;

    public AudioClip shipSaved;

    public AudioSource audio;

    public Pianola pianola;

    public StartRoundCanvasController startRound;

    public LivesController livesController;

    public CanvasGroup gameOver;

    public CanvasGroup nextWave;

    public CanvasGroup inGameUI;

    public float gameStartDelay = 2f;

    public int currentRound = 1;

    public int shipsToRescue = 5;

    public int shipsSaved = 0;

    bool NextWaveShowing { get { return nextWave.alpha == 1f; } }

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
        if (!isRunning || NextWaveShowing) return;

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
        if (!isRunning || NextWaveShowing) return;

        gameBoard.SetValueAt(rocketColumn, 5, false);

        AudioClip soundToPlay = playerMove;

        rocketColumn++;
        if (rocketColumn > 4)
        {
            // Add one to the # ships saved
            shipsSaved++;

            soundToPlay = shipSaved;

            // Check for level win condition
            if (shipsSaved == shipsToRescue)
            {
                StartNextWave();
                return;
            }

            // Move the player back to column 0
            rocketColumn = 0;
        }
        gameBoard.SetValueAt(rocketColumn, 5, true);
        audio.PlayOneShot(soundToPlay);
    }

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    IEnumerator Start()
    {
        yield return StartTheGame();
    }

    void StartNextWave()
    {
        StartCoroutine(MoveToNextWave());
    }

    IEnumerator StartTheGame()
    {
        inGameUI.alpha = 1f;
        nextWave.alpha = 0f;
        gameOver.alpha = 0f;

        rocketColumn = 0;

        if (PlayerPrefs.HasKey("currentRound"))
        {
            currentRound = PlayerPrefs.GetInt("currentRound", 1);
            if (currentRound == 6) currentRound = 1;
        }

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

        inGameUI.alpha = 0f;
        nextWave.alpha = 0f;
        yield return new WaitForSeconds(2);

        yield return StartTheGame();
    }

    IEnumerator MoveToNextWave()
    {
        pianola.Reset();
        gameBoard.Clear();
        inGameUI.alpha = 0f;
        gameOver.alpha = 0f;
        nextWave.alpha = 1f;
        yield return new WaitForSeconds(4);

        PlayerPrefs.SetInt("currentRound", currentRound + 1);

        SceneManager.LoadScene("Game");
    }

    IEnumerator GameOver()
    {
		// GAME OVER MAN, GAME OVER! -- Bill Paxton
		gameBoard.Clear();
        inGameUI.alpha = 0f;
        nextWave.alpha = 0f;
        gameOver.alpha = 1f;
		yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");
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
