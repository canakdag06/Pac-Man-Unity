using System.Collections;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] private SpriteAnimation movementAnimation;
    [SerializeField] private SpriteAnimation deathAnimation;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    public Movement Movement { get; private set; }

    private void OnEnable()
    {
        GameEvents.OnPacmanEaten += StartDeathAnimation;
    }

    private void OnDisable()
    {
        GameEvents.OnPacmanEaten -= StartDeathAnimation;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        Movement = GetComponent<Movement>();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();
        circleCollider.enabled = true;
        movementAnimation.StartAnimation();
    }

    private void StartDeathAnimation()
    {
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        Movement.enabled = false;
        circleCollider.enabled = false;

        movementAnimation.StopAnimation();

        deathAnimation.loop = false;
        deathAnimation.StartAnimation();

        float duration = (float)deathAnimation.sprites.Length / deathAnimation.FPS;
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
