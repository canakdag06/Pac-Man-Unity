using System.Collections;
using UnityEngine;

public class GhostHomeState : GhostBaseState
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(ExitHomeAnimation());
        }
    }

    private IEnumerator ExitHomeAnimation()
    {
        Ghost.Movement.SetDirection(Vector2.up, true);
        Ghost.Movement.rb.bodyType = RigidbodyType2D.Kinematic;
        Ghost.Movement.enabled = false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector2.Lerp(position, inside.position, elapsed / duration);
            elapsed += Time.deltaTime;
            Ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0.0f;
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector2.Lerp(inside.position, outside.position, elapsed / duration);
            elapsed += Time.deltaTime;
            Ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        Ghost.Movement.rb.bodyType = RigidbodyType2D.Dynamic;
        Ghost.Movement.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Ghost.Movement.SetDirection(-Ghost.Movement.Direction);
        }
    }
}
