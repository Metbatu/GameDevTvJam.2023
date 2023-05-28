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
    private bool isFalling = false;
    private bool hasLanded = false;
    private int jumpsRemaining;
    private float jumpTime = 0f;

    // Running variables
    public float initialSpeed = 5f;
    public float speedIncrement = 0.2f;
    public float maxSpeed = 10f;
    private Vector2 move;
    private Rigidbody2D rb;
    private float currentSpeed;

    // Animator
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentSpeed = initialSpeed;
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        HandleInput();
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        flip();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            isJumpButtonPressed = true;
            if (jumpsRemaining == maxJumps || hasLanded)
            {
                isJumping = true;
                hasLanded = false;
                jumpTime = Time.time;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpsRemaining--;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpButtonPressed = false;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(move.x * currentSpeed, rb.velocity.y);

        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement * Time.fixedDeltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
    }

    private void Jump()
    {
        if (isJumpButtonPressed && Time.time - jumpTime < jumpHoldDuration)
        {
            rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
        }

        if (rb.velocity.y > 0)
        {
            rb.gravityScale = 2.5f;
        }
        else
        {
            rb.gravityScale = 5f;
        }

        if (!isJumpButtonPressed && rb.velocity.y < 0)
        {
            isFalling = true;
        }
    }

    private void flip()
    {
        if (move.x < -0.01f) transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
        if (move.x > 0.01f) transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpsRemaining = maxJumps;
            currentSpeed = initialSpeed;
            if (isFalling)
            {
                animator.SetTrigger("Land");
                hasLanded = true;
                isFalling = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            IncreaseSpeed();
            Destroy(other.gameObject);
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(move.x));
        animator.SetBool("IsJumping", isJumping);
    }

    private void IncreaseSpeed()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement;
        }
    }
}
