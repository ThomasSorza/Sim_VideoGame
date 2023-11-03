    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sprite;
        private Animator anim;

        private float dirX = 0f;
        [SerializeField] private float moveSpeed = 7f; //to edit during runtime
        [SerializeField] private float jumpForce = 14f;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            dirX = Input.GetAxisRaw("Horizontal"); //moving left and right

            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump")) //jumping
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        {
            if (dirX > 0f) //moving right
            {
                anim.SetBool("running", true);
                sprite.flipX = false;
            }
            else if (dirX < 0f) //moving left
            {
                anim.SetBool("running", true);
                sprite.flipX = true;
            }
            else // not moving (idle)
            {
                anim.SetBool("running", false);
            }
        }
    }