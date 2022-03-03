using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [Header("References")]
    [SerializeField] Transform moveTo;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    public float xRotation { get; private set; }
    public float yRotation { get; private set; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        this.transform.position = moveTo.position;
        MyInput();

        transform.GetChild(0).transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.parent.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90);
    }
}
