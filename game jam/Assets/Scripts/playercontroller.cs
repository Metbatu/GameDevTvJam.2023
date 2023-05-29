using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpHoldForce = 2.5f;
    public float jumpHoldDuration = 0.1f;
    public int maxJumps = 2;
    private bool isJumpButtonPressed = false;
    private bool isJumping = false;
    private bool hasLanded = false;
    private float jumpTime = 0.1f;

    // Running variables
    public float initialSpeed = 3.5f;
    public float speedIncrement = 2.5f;
    public float maxSpeed = 8.0f;
    private Vector2 move;
    private Rigidbody2D rb;
    private float currentSpeed;

    // Animator
    private Animator animator;

    private void Awake()
    {                            //Brings the components used in unity
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        UpdateAnimator();
        HandleInput();
        scale();
        Jump();
    }
    private void scale()
    {
        transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpButtonPressed = true;
            if (hasLanded)
            {
                isJumping = true;
                hasLanded = false;
                jumpTime = Time.time;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    private void UpdateAnimator()
    {
        animator.SetBool("IsJumping", isJumping);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetTrigger("Land");
            hasLanded = true;
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
    }
}