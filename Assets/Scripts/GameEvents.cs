using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Pellet> OnPelletEaten;
    public static event Action<Ghost> OnGhostEaten;
    public static event Action OnPacmanEaten;
    public static event Action<int> OnScoreChanged;

    // Invoke Functions (not necessary but looks good)
    public static void PelletEaten(Pellet pellet)
    {
        OnPelletEaten?.Invoke(pellet);
    }

    public static void GhostEaten(Ghost ghost)
    {
        OnGhostEaten?.Invoke(ghost);
    }

    public static void PacmanEaten()
    {
        OnPacmanEaten?.Invoke();
    }

    public static void ScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
}
