using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Pacman pacman;
    public Ghost[] ghosts;
    public Transform pellets;
    private int pelletCount;

    private Coroutine powerupCoroutine;
    private int ghostScoreMultiplier = 1;
    private const int extraLifeScore = 10000;
    private const int initialLives = 4;
    private bool isExtraLifeGiven = false;

    public int Lives { get; private set; }
    public int Score { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        NewGame();
    }

    private void OnEnable()
    {
        GameEvents.OnPelletEaten += HandlePelletEaten;
        GameEvents.OnGhostEaten += GhostEaten;
        GameEvents.OnPacmanEaten += PacmanEaten;
    }

    private void OnDisable()
    {
        GameEvents.OnPelletEaten -= HandlePelletEaten;
        GameEvents.OnGhostEaten -= GhostEaten;
        GameEvents.OnPacmanEaten -= PacmanEaten;
    }

    private void NewGame()
    {
        SetLives(initialLives);
        SetScore(0);
        NewRound();
    }

    private void SetLives(int lives)
    {
        Lives = lives;
        GameEvents.LivesChanged(lives);
    }

    private void SetScore(int score)
    {
        Score = score;
        GameEvents.ScoreChanged(score);

        if (!isExtraLifeGiven)
        {
            if (score >= extraLifeScore)
            {
                SetLives(Lives + 1);
                isExtraLifeGiven = true;
            }
        }
    }

    private void NewRound()
    {
        pelletCount = 0;
        foreach (Transform pellet in pellets)
        {
            pelletCount++;
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostScoreMultiplier;
        SetScore(Score + points);
        GameEvents.TriggerGhostScore(points, ghost.transform.position);
        ghostScoreMultiplier++;
    }

    public void PacmanEaten()
    {
        SetLives(Lives - 1);

        if (Lives > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            GameOver();
        }

    }

    private void HandlePelletEaten(Pellet pellet)
    {
        pelletCount--;
        SetScore(Score + pellet.Points);
        if (pelletCount == 0)
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }

        // if the pellet is a PowerPellet
        if (pellet is PowerPellet powerPellet)
        {
            if (powerupCoroutine != null)
            {
                StopCoroutine(powerupCoroutine);
            }
            powerupCoroutine = StartCoroutine(PowerupTimer(((PowerPellet)pellet).PowerupDuration));

            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].ChaseState.Disable();
                ghosts[i].ScatterState.Disable();
                ghosts[i].FrightenedState.Enable(((PowerPellet)pellet).PowerupDuration);

            }
        }
    }

    private IEnumerator PowerupTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetMultiplier();
        powerupCoroutine = null;
    }

    private void ResetMultiplier()
    {
        ghostScoreMultiplier = 1;
    }

}
