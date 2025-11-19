using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public Pacman pacman;
    public Ghost[] ghosts;
    public Transform pellets;
    private int pelletCount;

    public int Lives { get; private set; }
    public int Score { get; private set; }

    void Start()
    {
        NewGame();
    }

    private void OnEnable()
    {
        GameEvents.OnPelletEaten += HandlePelletEaten;
    }

    private void OnDisable()
    {
        GameEvents.OnPelletEaten -= HandlePelletEaten;
    }

    private void NewGame()
    {
        SetLives(3);
        SetScore(0);
        NewRound();
    }

    private void SetLives(int lives)
    {
        Lives = lives;
    }

    private void SetScore(int score)
    {
        Score = score;
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
            ghost.gameObject.SetActive(true);
        }

        pacman.gameObject.SetActive(true);
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
        SetScore(Score + ghost.points);
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);

        SetLives(Lives - 1);

        if(Lives > 0)
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
        if(pelletCount == 0)
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }

        // if the pellet is a PowerPellet
        if (pellet is PowerPellet powerPellet)
        {
            // todo
        }
    }

}
