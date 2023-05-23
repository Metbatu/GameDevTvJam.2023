using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;        // Character movement speed
    public float jumpForce = 5f;        // Force applied when jumping
    public Transform groundCheck;       // Transform representing the position of the ground check object
    public LayerMask groundLayer;       // Layer mask for identifying the ground

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isOnGround = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the character is on the ground
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Character movement
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Jumping
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        // Apply jump force if the character is jumping
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }
}
