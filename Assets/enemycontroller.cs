using UnityEngine;

public class enemycontroller : MonoBehaviour
{
    public float speed = 2f;        // Speed of the enemy
    public float health = 100f;    // Starting health of the enemy
    public Transform groundCheck;  // Position to check for ground
    public LayerMask groundLayer;  // Layer to consider as ground

    private Rigidbody2D rb;        // Reference to the Rigidbody2D component
    private bool isFacingRight = true;  // Flag to keep track of enemy's facing direction
    private float maxHealth;        // Maximum health of the enemy

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    void FixedUpdate()
    {
        // Check if enemy is grounded
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // If enemy is not grounded, flip its direction
        if (!isGrounded)
        {
            FlipDirection();
        }

        // Move the enemy in its current direction
        float moveDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    void FlipDirection()
    {
        // Reverse the direction flag
        isFacingRight = !isFacingRight;

        // Flip the enemy's sprite horizontally
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    public void TakeDamage(float damage)
    {
        // Reduce the enemy's health by the damage amount
        health -= damage;

        // Clamp the health to 0 - maxHealth range
        health = Mathf.Clamp(health, 0f, maxHealth);

        if (health == 0f)
        {
            // If the enemy's health reaches 0, destroy the GameObject
            Destroy(gameObject);
        }
    }
}