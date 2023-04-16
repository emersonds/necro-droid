using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float jumpTime = 0.5f;
    private float jumpTimeCounter;

    // Ground detection variables
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius = 0.1f;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool isGrounded;

    // Ceiling detection variables
    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private float ceilingCheckRadius = 0.1f;
    private bool isTouchingCeiling;

    // Rigidbody2D component reference
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        jumpTimeCounter = jumpTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Check if the player is touching a ceiling
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsGround);

        // Handle horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isTouchingCeiling)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && jumpTimeCounter > 0 && !isTouchingCeiling)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || isTouchingCeiling)
        {
            jumpTimeCounter = 0;
        }

        if (isGrounded)
        {
            jumpTimeCounter = jumpTime;
        }
    }
}
