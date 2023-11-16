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
    private float velocidadOriginal;

    // Ralentización variables
    private float factorDeRalentizacionActual = 1.0f; // Valor por defecto

    private enum MovementState { idle, running, jumping, falling, knockback }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        velocidadOriginal = moveSpeed;
    }

    void Update()
    {
        if (KBCounter <= 0)
        {
            dirX = Input.GetAxisRaw("Horizontal"); // moving left and right

            rb.velocity = new Vector2(dirX * moveSpeed * factorDeRalentizacionActual, rb.velocity.y);

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
        }
        else if (dirX < 0f) // moving left
        {
            facingRight = false;
            Flip(false);
            state = MovementState.running;
        }
        else // not moving (idle)
        {
            state = MovementState.idle;
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

    private void Flip(bool facingRight)
    {
        if (flippedLeft && facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = false;
        }
        if (!flippedLeft && !facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = true;
        }
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

    public void Ralentizar(float factor)
    {
        // Llama a esta función desde el script del agua
        factorDeRalentizacionActual = factor;
        moveSpeed = velocidadOriginal * factorDeRalentizacionActual;
    }

    public void RestablecerVelocidad()
    {
        // Restablece la velocidad a su valor original
        moveSpeed = velocidadOriginal;
    }
}
