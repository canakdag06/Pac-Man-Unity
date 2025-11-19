using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = spawnPoint.position;
    }
}
