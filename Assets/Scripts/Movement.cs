using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 initialDirection;
    private Vector2 nextDirection;
    private Vector2 direction;
    private Vector3 startingPos;

    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
    }

    private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.MoveEvent += HandleMovement;
        }
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.MoveEvent -= HandleMovement;
        }
    }

    void Start()
    {
        ResetState();
    }

    void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    public void ResetState()
    {
        speedMultiplier = 1.0f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPos;
        rb.bodyType = RigidbodyType2D.Dynamic;
        this.enabled = true;
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = speed * speedMultiplier * Time.fixedDeltaTime * direction;

        rb.MovePosition(position + translation);
    }

    private void HandleMovement(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        Vector2 dir = Vector2.zero;

        if (direction.y > 0f)
        {
            dir = Vector2.up;
        }
        else if (direction.y < 0f)
        {
            dir = Vector2.down;
        }
        else if (direction.x < 0f)
        {
            dir = Vector2.left;
        }
        else if (direction.x > 0f)
        {
            dir = Vector2.right;
        }

        SetDirection(dir);
    }

    public void SetDirection(Vector2 dir, bool forced = false)
    {
        if (IsTileValid(dir) || forced)
        {
            direction = dir;
            nextDirection = Vector2.zero;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            nextDirection = dir;
        }
    }

    public bool IsTileValid(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, dir, 1.5f, wallLayer);
        return hit.collider == null;
    }
}
