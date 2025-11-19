using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Pellet> OnPelletEaten;
    //public static event Action<PowerPellet> OnPowerPelletEaten;



    // Invoke Functions (not necessary but looks good)
    public static void PelletEaten(Pellet pellet)
    {
        OnPelletEaten?.Invoke(pellet);
    }

    //public static void PowerPelletEaten(PowerPellet pellet)
    //{
    //    OnPowerPelletEaten?.Invoke(pellet);
    //}
}
