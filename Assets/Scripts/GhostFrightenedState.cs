using UnityEngine;

public class GhostFrightenedState : GhostBaseState
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer frightened;
    public SpriteRenderer frightenedExpiring;

    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        frightened.enabled = true;
        frightenedExpiring.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        frightened.enabled = false;
        frightenedExpiring.enabled = false;
    }

    private void Flash()
    {
        if(!eaten)
        {
            frightened.enabled = false;
            frightenedExpiring.enabled = true;
            frightenedExpiring.GetComponent<SpriteAnimation>().Restart();
        }
    }

    private void GetEaten()
    {
        eaten = true;

        Vector3 position = this.Ghost.HomeState.inside.position;
        position.z = this.Ghost.transform.position.z;
        this.Ghost.transform.position = position;
        Ghost.HomeState.Enable(this.duration);

        body.enabled = false;
        eyes.enabled = true;
        frightened.enabled = false;
        frightenedExpiring.enabled = false;
    }

    private void OnEnable()
    {
        this.Ghost.Movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        this.Ghost.Movement.speedMultiplier = 1.0f;
        eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if(this.enabled)
            {
                GetEaten();
            }
        }
    }
}
