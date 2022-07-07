using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Controlled values
    [Header("Movement")]
    [SerializeField]
    private int speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float maxJumpTime;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private float coyoteTime;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float floatingSpeed;
    [Header("Abilities")]
    [SerializeField]
    private bool enableWalljump;
    [SerializeField]
    private bool enableFloating;
    [Header("Attack")]
    [SerializeField]
    private Attack attacker;

    // Player conponents
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    // Create player movement objects
    private PlayerInput playerInput;
    private InputAction movement;
    private InputAction jump;
    private InputAction floating;

    // Jumping variables
    private float dGrounded = 0f;
    private float jumpTime;
    private bool jumping = false;

    private void Awake() {
        // Instantiate player movement
        playerInput = new PlayerInput();
    }

    private void OnEnable() {
        // Get player input values
        movement = playerInput.Player.Walk;
        movement.Enable();
        jump = playerInput.Player.Jump;
        jump.Enable();
        floating = playerInput.Player.Float;
        floating.Enable();
        playerInput.Player.Attack.performed += attack;
        playerInput.Player.Attack.Enable();

        // Get player rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
        // Get player collider
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnDisable() {
        // Disable player movement (prevents errors)
        movement.Disable();
        jump.Disable();
        playerInput.Player.Attack.Disable();
    }

    private void FixedUpdate() {
        if (isGrounded()) {
            dGrounded = 0f;
        } else if (jumping) {
            dGrounded += coyoteTime;
        } else {
            dGrounded += Time.deltaTime;
        }
        checkJump();
        checkFloating();
        doMovement();
    }

    // Make the player jump if on ground, higher jump when holding button
    // Controlled by maxJumpTime  & jumpForce variables
    private void checkJump() {
        if (jump.ReadValue<float>() != 0) {
            if (!jumping && (dGrounded <= coyoteTime)) {
                jumping = true;
                jumpTime = 0;
            } else if (jumping && jumpTime < maxJumpTime) {
                jumpTime += Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //transform.Translate(new Vector3(0, jumpForce, 0) * Time.deltaTime);
            }
        } else {
            jumping = false;
        }
    }

    // Set velocity to -1 * floatingSpeed if the key is pressed
    private void checkFloating() {
        if (!enableFloating) {
            return;
        }

        if (floating.ReadValue<float>() != 0) {
            rb.velocity = new Vector2(rb.velocity.x, -1f * floatingSpeed);
        }
    }

    // Check if the player is on the ground or wall
    // Min ground distance is controlled by groundDistance variable
    private bool isGrounded() {
        RaycastHit2D groundCheckLeft = Physics2D.Raycast(new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D groundCheckRight = Physics2D.Raycast(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y), Vector2.down, groundDistance, groundLayer);
        bool groundCheck = (groundCheckLeft.collider != null) || (groundCheckRight.collider != null);
        if (enableWalljump) {
            RaycastHit2D wallCheckLeft = Physics2D.Raycast(playerCollider.bounds.center, new Vector2(1.0f, 0.0f), playerCollider.bounds.extents.x + groundDistance, groundLayer);
            RaycastHit2D wallCheckRight = Physics2D.Raycast(playerCollider.bounds.center, new Vector2(-1.0f, 0.0f), playerCollider.bounds.extents.x + groundDistance, groundLayer);
            return (groundCheck | wallCheckLeft.collider != null | wallCheckRight.collider != null);
        }
        return groundCheck;
    }

    // Move the player (depending on speed variable)
    private void doMovement() {
        float xAxis = movement.ReadValue<float>();
        gameObject.transform.position = new Vector2 (transform.position.x + (xAxis * ((float)speed / 500f)), transform.position.y);
        checkFlip(xAxis);
    }

    // Check if the player needs to flip direction
    private void checkFlip(float xAxis) {
        if (xAxis < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (xAxis > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Attack of not yet attacking
    private void attack(InputAction.CallbackContext obj) {
        attacker.hitCollisions();
    }
}
