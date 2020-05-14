using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // The speed the player moves
    public float speed;

    public float jumpForce;

    public Animator animator;

    public Rigidbody2D rb;

    private Vector3 zeroVelocity = Vector3.zero;

    // The horizontal movement of the player
    private float horizontalMove;

    // Whether the player is walking or not
    private bool walking = false;

    private float xScale;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    void Start()
    {
        xScale = transform.localScale.x;
    }

    void Update()
    {
        UpdateMovement();

        UpdateDirection();
    }

    void FixedUpdate() {

        if (walking) {

            // Set the target velocity to the horizontal movement reading
            Vector3 targetVelocity = new Vector2(horizontalMove * speed, rb.velocity.y);

            // Smooth and move
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);
        }
    }

    void UpdateMovement() {

        // Get horizontal player input
        horizontalMove = Input.GetAxisRaw("Horizontal");

        // Set animator values if player is moving
        if (horizontalMove != 0) {

            // Set animator parameter
            animator.SetFloat("Horizontal", horizontalMove);

        } else {

            // Stop walking if not moving
            walking = false;
        }

        // Set the speed of the character in order to determine whether to move or not
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // If grounded and space key pressed, jump
        if (grounded() && Input.GetKeyDown("space")) {
            animator.SetBool("Jumping", true);
            rb.AddForce(new Vector2(0, jumpForce));
        } else {
            animator.SetBool("Jumping", false);
        }
    }

    void UpdateDirection() {

        // Update the direction the player is facing based on movement
        if (horizontalMove == 1) transform.localScale = new Vector3(-1 * xScale, transform.localScale.y, transform.localScale.z);
        if (horizontalMove == -1) transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }

    void StartWalk() {
        walking = true;
    }

    // Whether the player is on the ground
    bool grounded() {
        if (rb.velocity.y == 0) return true;
        return false;
    }

}
