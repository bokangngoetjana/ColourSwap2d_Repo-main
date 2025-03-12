using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 0.8f; //jump height
    public SpriteRenderer sr;
    private Rigidbody2D rb;
    private Color[] colors = new Color[4];
    private int currentIndex = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


        ColorUtility.TryParseHtmlString("#75D5FD", out colors[0]);
        ColorUtility.TryParseHtmlString("#B76CFD", out colors[1]);
        ColorUtility.TryParseHtmlString("#FF2281", out colors[2]);
        ColorUtility.TryParseHtmlString("#011FFD", out colors[3]);

        sr.color = colors[currentIndex];
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tap or Click to swap color & jump
        {
            ChangeColor();
            Jump();
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput * 1.5f * Time.deltaTime, 0, 0);

        // Prevent the ball from going off-screen
        /*if (transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }*/
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), Mathf.Clamp(transform.position.y, 2, 5), transform.position.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            SpriteRenderer obstacleColor = collision.GetComponent<SpriteRenderer>();

            if (obstacleColor.color == sr.color)
            {
                ScoreManager.instance.AddScore();
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0;
            }
        }
    }
    void ChangeColor()
    {
        currentIndex = (currentIndex + 1) % colors.Length;  // Cycle through all colors
        sr.color = colors[currentIndex];  // Apply new color
        Debug.Log("Color changed to: " + sr.color);  // Debugging (check Console)
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Set velocity only in Y direction
    }
}
