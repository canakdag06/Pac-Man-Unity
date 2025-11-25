using UnityEngine;

public class GhostChaseState : GhostBaseState
{
    private void OnDisable()
    {
        Ghost.ScatterState.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (enabled && !Ghost.FrightenedState.enabled && node != null)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

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
}
