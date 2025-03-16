using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalSpeed = 2f;
    public float horizontalSpeed = 3f;
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    private int currentIndex = 0;
    private Color[] colors = new Color[4];
    [SerializeField] private GameObject explosionPrefab;

    private float maxSpeed = 6f;
    private int lastScoreCheckpoint = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        ColorUtility.TryParseHtmlString("#75D5FD", out colors[0]);
        ColorUtility.TryParseHtmlString("#B76CFD", out colors[1]);
        ColorUtility.TryParseHtmlString("#FF2281", out colors[2]);
        ColorUtility.TryParseHtmlString("#011FFD", out colors[3]);

        sr.color = colors[currentIndex];

        if (GameModeManager.isClassicMode)
        {
            rb.linearVelocity = new Vector2(0, verticalSpeed); // Move up only
        }
        else
        {
            rb.linearVelocity = new Vector2(verticalSpeed, rb.linearVelocity.y); // Sidebars Mode
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tap or Click to swap color
        {
            ChangeColor();
        }

        if (GameModeManager.isClassicMode)
        {
            HandleClassicModeMovement();
        }

        AdjustSpeedBasedOnScore();
    }

    private void HandleClassicModeMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * horizontalSpeed, rb.linearVelocity.y); // Allow left/right movement

        // Prevent player from moving off-screen
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float clampedX = Mathf.Clamp(transform.position.x, -screenHalfWidth + 0.5f, screenHalfWidth - 0.5f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void AdjustSpeedBasedOnScore()
    {
        int currentScore = ScoreManager.instance.score;

        if (currentScore >= lastScoreCheckpoint + 5)
        {
            verticalSpeed = Mathf.Min(verticalSpeed + 0.6f, maxSpeed);
            lastScoreCheckpoint = currentScore;

            if (GameModeManager.isClassicMode)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalSpeed); // Increase only vertical speed
            }
            else
            {
                rb.linearVelocity = new Vector2(verticalSpeed * Mathf.Sign(rb.linearVelocity.x), rb.linearVelocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            SpriteRenderer obstacleColor = collision.GetComponent<SpriteRenderer>();

            if (obstacleColor.color == sr.color)
            {
                GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                ScoreManager.instance.AddScore();
                Destroy(collision.gameObject);
                Destroy(explosion, 1f);
            }
            else
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0;
            }
        }

        // 🔥 FIX: Sidebars Mode Bounce Still Works!
        if (!GameModeManager.isClassicMode && collision.CompareTag("Trigger"))
        {
            rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }

    void ChangeColor()
    {
        currentIndex = (currentIndex + 1) % colors.Length;  // Cycle through colors
        sr.color = colors[currentIndex];  // Apply new color
    }
}
