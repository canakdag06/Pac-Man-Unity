using UnityEngine;

public class GhostChaseStateClyde : GhostChaseState
{
    private const float chaseDistance = 64f; // assigned 8^2 to use sqrMagnitude for optimization
    private Vector3 corner;

    private void Start()
    {
        corner = Ghost.ScatterState.Corner.transform.position;
    }

    protected override Vector3 GetTargetPosition()
    {
        Vector3 pacmanPosition = Ghost.target.transform.position;
        Vector3 clydePosition = transform.position;

        float distance = (clydePosition - pacmanPosition).sqrMagnitude;

        if (distance > chaseDistance)
        {
            return pacmanPosition;
        }
        else
        {
            return corner;
        }
    }
}
