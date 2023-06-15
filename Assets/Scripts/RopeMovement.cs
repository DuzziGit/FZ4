using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float climbSpeed = 4f;
    public float exitBoostForce = 80f;

    // Unity Components
    private Rigidbody2D playerRb;

    // States
    private bool isOnLadder;
    private bool isClimbing;
    private float climbInput;
    private bool jumpRequested;

    private float originalGravityScale;
    private float originalDrag;
    private GameObject currentRope;

    private void Awake()
    {
        // Initialize references
        playerRb = GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Start()
    {
        originalGravityScale = playerRb.gravityScale;
        originalDrag = playerRb.drag;
    }

    private void Update()
    {
        // Get climb input
        climbInput = Input.GetAxisRaw("Vertical");

        // Set isClimbing based on presence of a ladder and input
        isClimbing = isOnLadder && Mathf.Abs(climbInput) > 0f && Input.GetKey(KeyCode.UpArrow);

        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        // Check if the spacebar is pressed while climbing
        if (jumpRequested && isOnLadder && Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("POGPOG");
            playerRb.gravityScale = originalGravityScale;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Determine the launch direction based on the arrow key being held
            float launchDirection = 0f;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                launchDirection = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                launchDirection = 1f;
            }

            // Launch the character using the specified force in the desired direction
            Jump(launchDirection);
            jumpRequested = false;
        }

        if (isClimbing)
        {
            // Lock the player's movement on the X-axis while climbing
            playerRb.velocity = new Vector2(0f, climbInput * climbSpeed);
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        else if (isOnLadder)
        {
            // If the player is on the rope but not moving, they should stay still
            playerRb.velocity = Vector2.zero;
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            // Re-enable gravity and rotation constraints
            playerRb.gravityScale = originalGravityScale;
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void Jump(float launchDirection)
    {
        // Apply a small upward boost when jumping off the rope
        playerRb.velocity = new Vector2(playerRb.velocity.x, exitBoostForce);

        // Apply a launch force in the specified direction
        playerRb.AddForce(new Vector2(launchDirection * exitBoostForce, 0f), ForceMode2D.Impulse);

        // Unlock the X position for the jump
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope"))
        {
            isOnLadder = true;

            // Store the current rope
            currentRope = collision.gameObject;

            // Center the player on the rope
            playerRb.transform.position = new Vector2(currentRope.transform.position.x, playerRb.transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope"))
        {
            isOnLadder = false;

            // Reset the gravity scale to the original value when exiting the ladder
            playerRb.gravityScale = originalGravityScale;

            // Clear the current rope reference
            currentRope = null;
        }
    }
}
