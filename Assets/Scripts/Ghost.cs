using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public GhostHomeState HomeState { get; private set; }
    public GhostScatterState ScatterState { get; private set; }
    public GhostChaseState ChaseState { get; private set; }
    public GhostFrightenedState FrightenedState { get; private set; }
    public GhostBaseState baseState;

    public Transform target;
    public int points = 200;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        HomeState = GetComponent<GhostHomeState>();
        ScatterState = GetComponent<GhostScatterState>();
        ChaseState = GetComponent<GhostChaseState>();
        FrightenedState = GetComponent<GhostFrightenedState>();
    }


    void Start()
    {
        ResetState();
    }


    void Update()
    {

    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();

        FrightenedState.Disable();
        ChaseState.Disable();

        if (HomeState != baseState)
        {
            HomeState.Disable();
        }

        if (baseState != null)
        {
            baseState.Enable();
        }

        ScatterState.Enable();
    }
}
