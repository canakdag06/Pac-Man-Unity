using UnityEngine;

public abstract class GhostBaseState : MonoBehaviour
{
    public Ghost Ghost { get; private set; }

    public float duration;

    private void Awake()
    {
        Ghost = GetComponent<Ghost>();
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
