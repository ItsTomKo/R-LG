using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    public LayerMask ground;

    [Header("Controlls")]
    public KeyCode sprint;
    public KeyCode crouch;
    [Header("Pickup")]
    public bool autoPickWhenNear;
    public float pickupSpeed;
    public float pickupMaxTime;
    public float pickDistance;
    public float pickupRadius;
    public KeyCode pickupKeybind;
    public LayerMask pickable;


    void Update()
    {
        movement.LocalInstance.Move();
    }
}
