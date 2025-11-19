using UnityEngine;

public class Pellet : MonoBehaviour
{
    private int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {

        }
    }
}
