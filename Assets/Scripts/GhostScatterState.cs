using UnityEngine;

public class GhostScatterState : GhostBaseState
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (enabled && !Ghost.FrightenedState.enabled && node != null)
        {
            int index = Random.Range(0, node.AvailableDirections.Count);

            if (node.AvailableDirections.Count > 1 && node.AvailableDirections[index] == -Ghost.Movement.Direction)
            {
                index++;

                if (index >= node.AvailableDirections.Count)
                {
                    index = 0;
                }
            }

            Ghost.Movement.SetDirection(node.AvailableDirections[index]);
        }
    }
}
