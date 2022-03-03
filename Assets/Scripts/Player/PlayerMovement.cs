using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Floats")]
    public float movementMultiplier;
    public float counterMovementMultiplier;
    public float moveSpeed;
    public float sprintSpeed;
    public float maxSpeed;
    public float jumpHeight;
    public float groundCheckDistance;
    public float gravity;
    [Header("References")]
    public PlayerController con;
    public LayerMask ground;
    public Rigidbody rb;
    public Transform orientation;
    public Transform groundCheck;

    [HideInInspector]
    public PlayerMovement LocalInstance;

    Vector3 moveDir;
    public bool isJumping, isGrounded,isSprinting;

    float horizontal, vertical;

    void Awake()
    {
        if (LocalInstance == null)
            LocalInstance = this;
    }

    public void Move()
    {
        //Add Gravity
        rb.AddForce(Vector3.down * gravity);

        //Get Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isJumping = Input.GetButton("Jump");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance,ground);
        isSprinting = Input.GetButton("Sprint");

        //Calculate Move Direction
        moveDir = orientation.forward * vertical + orientation.right * horizontal;

        //Add force
        rb.AddForce(moveDir * ((isSprinting) ? sprintSpeed : moveSpeed) * movementMultiplier);
        if (isGrounded && isJumping)
            rb.AddForce(Vector3.up * jumpHeight * movementMultiplier,ForceMode.Force);
        CounterMovement();
    }
    void CounterMovement()
    {
        Vector3 vel = rb.velocity;
        vel.y = 0f;

        float coefficientOfFriction = (moveSpeed * counterMovementMultiplier) / maxSpeed;

        rb.AddForce(-vel * coefficientOfFriction, ForceMode.Acceleration);
    }
}
