using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] private SpriteAnimation deathSequence;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();
    }
}
