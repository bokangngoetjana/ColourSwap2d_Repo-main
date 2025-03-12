using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed;
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    private int currentIndex = 0;
    private Color[] colors = new Color[4];
    [SerializeField] private GameObject explosionPrefab;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.linearVelocity = new Vector2(_speed, rb.linearVelocity.y);

        ColorUtility.TryParseHtmlString("#75D5FD", out colors[0]);
        ColorUtility.TryParseHtmlString("#B76CFD", out colors[1]);
        ColorUtility.TryParseHtmlString("#FF2281", out colors[2]);
        ColorUtility.TryParseHtmlString("#011FFD", out colors[3]);

        sr.color = colors[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tap or Click to swap color & jump
        {
            ChangeColor();
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
        if (collision.CompareTag("Trigger"))
        {
            if(rb.linearVelocity.x > 0.1f)
            {
                print("hmm");
                rb.linearVelocity = -new Vector2(_speed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(_speed, rb.linearVelocity.y);
            }
            _speed += 0.05f;
        }
    }
    void ChangeColor()
    {
        currentIndex = (currentIndex + 1) % colors.Length;  // Cycle through all colors
        sr.color = colors[currentIndex];  // Apply new color
    }
}
