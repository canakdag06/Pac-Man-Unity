using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> AvailableDirections { get; private set; }

    [SerializeField] private LayerMask obstacleLayer;

    void Start()
    {
        AvailableDirections = new List<Vector2>();

        CheckDirection(Vector2.up);
        CheckDirection(Vector2.down);
        CheckDirection(Vector2.left);
        CheckDirection(Vector2.right);
    }

    private void CheckDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, obstacleLayer);

        if (hit.collider == null)
        {
            AvailableDirections.Add(direction);
        }

    }
}
