using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Floats")]
    public float movementMultiplier;
    public float counterMovementMultiplier;
    public float currentSpeed;
    public float moveSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float maxSpeed;
    public float jumpHeight;
    public float groundCheckDistance;
    public float gravity;
    [Header("References")]
    public Transform gfx;
    public PlayerController con;
    public LayerMask ground;
    public Rigidbody rb;
    public Transform orientation;
    public Transform groundCheck;

    [HideInInspector]
    public PlayerMovement LocalInstance;

    Vector3 moveDir;
    public bool isJumping, isGrounded,isSprinting,isSliding;

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
        isSliding = Input.GetButton("Slide");

        //Calculate Move Direction
        moveDir = orientation.forward * vertical + orientation.right * horizontal;

        //Set Move Speed
        currentSpeed = rb.velocity.magnitude;
        if (currentSpeed % 1 != 0)
            Debug.Log("a");

        //Add force
        rb.AddForce(moveDir * ((isSprinting) ? sprintSpeed : moveSpeed) * movementMultiplier);
        if (isGrounded && isJumping)
            rb.AddForce(Vector3.up * jumpHeight * movementMultiplier,ForceMode.Force);
        if (isSliding)
            StartSlide();
        else StopSlide();
        CounterMovement();
    }
    void StartSlide()
    {
        gfx.transform.localScale = new Vector3(1, 0.5f, 1);
        rb.AddForce(orientation.forward * slideSpeed * 1000 * Time.deltaTime);
    }
    void StopSlide()
    {
        gfx.transform.localScale = new Vector3(1, 1, 1);
    }
    void CounterMovement()
    {
        Vector3 vel = rb.velocity;
        vel.y = 0f;

        float coefficientOfFriction = (moveSpeed * counterMovementMultiplier) / maxSpeed;

        rb.AddForce(-vel * coefficientOfFriction, ForceMode.Acceleration);
    }
}
