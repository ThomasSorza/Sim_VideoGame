using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    private Animator anim;


    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f; // to edit during runtime
    [SerializeField] private float jumpForce = 14f;

    // Knockback effect variables
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool isKnockedFromRight;
    public bool flippedLeft;
    public bool facingRight;

    private enum MovementState { idle, running, jumping, falling, knockback }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (KBCounter <= 0)
        {
            dirX = Input.GetAxisRaw("Horizontal"); // moving left and right

            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded()) // jumping
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();
        }
        else
        {
            // Apply knockback effect
            if (isKnockedFromRight)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            else
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
            anim.SetInteger("state", (int)MovementState.knockback);
        }
    }

    private void UpdateAnimationState()
{
    MovementState state;

    if (dirX > 0f) // moving right
    {
        facingRight = true;
        Flip(true);
        state = MovementState.running;
        //sprite.flipX = false;
    }
    else if (dirX < 0f) // moving left
    {
        facingRight = false;
        Flip(false);
        state = MovementState.running;
        //sprite.flipX = true;
    }
    else // not moving (idle)
    {
        state = MovementState.idle;
    }

    void Flip (bool facingRight){
        if(flippedLeft && facingRight){
            transform.Rotate(0,-180,0);
            flippedLeft = false;
        }
        if(!flippedLeft && !facingRight)
        {
            transform.Rotate(0,-180,0);
            flippedLeft = true;
        }
    }

    // We can jump whenever we are not falling or jumping
    if (rb.velocity.y > 0.1f)
    { // dealing with jumping and falling
        state = MovementState.jumping;
    }
    else if (rb.velocity.y < -0.1f) // falling
    {
        state = MovementState.falling;
    }
    anim.SetInteger("state", (int)state);
}

    private bool IsGrounded() // Check if the player is on the ground to just jump once
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        // Imagine a box (a little bit down the player); if the box is colliding with the ground, then we are grounded
    }

    public void ApplyKnockback(Vector2 direction)
    {
        KBCounter = KBTotalTime;
        isKnockedFromRight = direction.x < 0;
    }
}
