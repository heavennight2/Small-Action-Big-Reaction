
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 14f;
    public float fallMultiplier = 2.5f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public int maxJumps = 3;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool wasGrounded;
    private int jumpsRemaining;

    private float horizontalInput;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Refill jumps only when landing
        if (isGrounded && !wasGrounded)
            jumpsRemaining = maxJumps;

        wasGrounded = isGrounded;

        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );

        if (spriteRenderer != null)
        {
            if (horizontalInput > 0)
                spriteRenderer.flipX = false;
            else if (horizontalInput < 0)
                spriteRenderer.flipX = true;
        }

        if (jumpRequested && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            jumpsRemaining--;
        }

        jumpRequested = false;

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up
                * Physics2D.gravity.y
                * (fallMultiplier - 1f)
                * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}
