using System.Collections;
using UnityEngine;

public class GhostBaseState : MonoBehaviour
{
    public Ghost Ghost { get; private set; }

    [SerializeField] private float duration;

    private void Awake()
    {
        Ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}
