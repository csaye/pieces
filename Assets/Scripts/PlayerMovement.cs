using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // The speed the player moves
    public float speed;

    public Animator animator;

    public Rigidbody2D rb;

    // The movement vector of the player
    private Vector2 movement = Vector2.zero;

    // Whether the player is walking or not
    private bool walking = false;

    void Start()
    {

    }

    void Update()
    {
        UpdateMovement();

        UpdateDirection();
    }

    void FixedUpdate() {

        if (walking) {

            // Update player position based on player input
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void UpdateMovement() {

        // Get horizontal player input
        movement.x = Input.GetAxisRaw("Horizontal");

        // Set animator values if player is moving
        if (movement != Vector2.zero) {

            // Set animator parameter
            animator.SetFloat("Horizontal", movement.x);

        } else {

            // Stop walking if not moving
            walking = false;
        }

        // Set the speed of the character in order to determine whether to move or not
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void UpdateDirection() {

        // Update the direction the player is facing based on movement
        if (movement.x == 1) transform.localScale = new Vector3(-1, 1, 1);
        if (movement.x == -1) transform.localScale = new Vector3(1, 1, 1);
    }

    void StartWalk() {
        walking = true;
    }

}
