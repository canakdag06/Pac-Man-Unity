using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] private int points = 10;
    public int Points => points;

    protected virtual void GetEaten()
    {
        GameEvents.PelletEaten(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            GetEaten();
        }
    }
}
