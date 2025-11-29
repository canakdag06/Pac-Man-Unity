using UnityEngine;

public abstract class GhostChaseState : GhostBaseState
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private SpriteRenderer targetRenderer;

    public override void Enable(float duration)
    {
        base.Enable(duration);

        targetRenderer.enabled = true;
    }

    public override void Disable()
    {
        base.Disable();

        targetRenderer.enabled = false;

        if(!Ghost.FrightenedState.enabled)
        {
            Ghost.ScatterState.Enable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (Ghost.target == null)
        {
            Ghost.Movement.SetDirection(-Ghost.Movement.Direction);
            return;
        }

        if (enabled && !Ghost.FrightenedState.enabled && node != null)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            Vector3 targetPosition = GetTargetPosition();
            targetPos.transform.position = targetPosition;

            foreach (Vector2 dir in node.AvailableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0.0f);
                float distance = (Ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = dir;
                    minDistance = distance;
                }
            }
            Ghost.Movement.SetDirection(direction);
        }
    }

    protected abstract Vector3 GetTargetPosition();
}
