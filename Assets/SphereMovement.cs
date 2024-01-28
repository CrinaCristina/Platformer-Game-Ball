using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 startPosition;

    // Score Management
    public int score = 0;
    public Text scoreText;
    public TextMeshProUGUI winMessageText;

    // Coin Collection
    public string winSceneName = "Win"; // Name of the 'You Won' scene
    public TextMeshProUGUI collectedCoinsText; // UI Text for collected coins

    private int coinsCollected = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startPosition = transform.position;
        UpdateCollectedCoinsUI();
        CoinCollector.coinsCollected = 0;
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        isGrounded = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (transform.position.y < -10)
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0;
        Vector3 velocity = move.normalized * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void CollectCoin()
    {
        coinsCollected++;
        AddScore(1);
        UpdateCollectedCoinsUI();
    }

    void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= coinsCollected)
        {
            ShowWinMessage();
        }
    }

    void ShowWinMessage()
    {
        if (winMessageText != null)
        {
            winMessageText.text = "Congratulations! You collected " + coinsCollected + " coins!";
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void UpdateCollectedCoinsUI()
    {
        collectedCoinsText.text = coinsCollected.ToString();
    }
    public void UpdateScore(int newScore)
    {
        score = newScore;
        UpdateScoreUI();
        UpdateCollectedCoinsUI();
    }

}
