using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 Direction { get; private set; }
    public Rigidbody2D rb;
    public float speedMultiplier = 1.0f;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 initialDirection;
    private Vector2 nextDirection;
    private Vector3 startingPos;

    [SerializeField] private float speed;

    [SerializeField] private bool isSpriteRotating;

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
        wallLayer = LayerMask.GetMask("Wall");
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
        Direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPos;
        rb.bodyType = RigidbodyType2D.Dynamic;
        this.enabled = true;
        RotateSprite();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = speed * speedMultiplier * Time.fixedDeltaTime * Direction;

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
            Direction = dir;
            nextDirection = Vector2.zero;

            if (isSpriteRotating)
            {
                RotateSprite();
            }
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

    private void RotateSprite()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
