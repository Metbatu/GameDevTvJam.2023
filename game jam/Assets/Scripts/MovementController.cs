using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Jumping variables
    public float jumpForce = 10f;
    public float jumpHoldForce = 2.5f;
    public float jumpHoldDuration = 0.1f;
    public int maxJumps = 2;
    private bool isJumpButtonPressed = false;
    private bool isJumping = false;
    private int jumpsRemaining;
    private float jumpTime = 0f;

    // Running variables
    public float initialSpeed = 5f;
    public float speedIncrement = 0.2f;
    public float maxSpeed = 10f;
    private Vector2 move;
    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        HandleInput();
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        flip();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            isJumpButtonPressed = true;
            isJumping = true;
            jumpTime = Time.time;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpsRemaining--;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpButtonPressed = false;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(move.x * currentSpeed, rb.velocity.y);

        // Increase speed gradually until reaching the max speed
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement * Time.fixedDeltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
    }

    void Jump()
    {
        if (isJumpButtonPressed && Time.time - jumpTime < jumpHoldDuration)
        {
            rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
        }

        // Adjust gravity scale for a more responsive jump
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = 2.5f;
        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    void flip()
    {
        if (move.x < -0.01f) transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
        if (move.x > 0.01f) transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpsRemaining = maxJumps;
            currentSpeed = initialSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            IncreaseSpeed();
            // Here you can add any other logic you want to perform when the player collects a speed boost item
            Destroy(other.gameObject); // Destroy the speed boost item
        }
    }

    void IncreaseSpeed()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement;
        }
    }
}
