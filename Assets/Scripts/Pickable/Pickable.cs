using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public PlayerController con;
    public Transform player;
    public Transform pickupHolder;
    public Transform camera;
    bool isNear;
    bool isPicked;

    float startGrab;
    private void Update()
    {
        if (con == null)
            con = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        if (player == null)
            player = con.transform;
        if (camera == null)
            camera = GameObject.Find("Camera Holder").transform;
        if (pickupHolder == null)
            pickupHolder = GameObject.Find("PickupHolder").transform;

        if (Vector3.Distance(player.position, this.transform.position) <= con.pickDistance)
            isNear = true;
        else isNear = false;
        if (isNear)
            DetectPick();
        if (!isNear && isPicked)
            isPicked = false;
    }
    void DetectPick()
    {
        if (con.autoPickWhenNear)
        {
            if (isPicked)
                Hold();
            else
                Pick();
        }
        else if (Input.GetKey(con.pickupKeybind))
        {
            RaycastHit hit;
            if (!Physics.SphereCast(camera.transform.position, con.pickupRadius, camera.forward, out hit, con.pickDistance, con.pickable))
            {
                if (isPicked)
                    Hold();
                else
                    Pick();
            }
        }
        else LetGo();
    }
    void Pick()
    {
        Rigidbody rb;
        if (transform.GetComponent<Rigidbody>() == null)
            rb = gameObject.AddComponent<Rigidbody>();
        else rb = transform.GetComponent<Rigidbody>();
        Collider coll = GetComponent<Collider>();
        coll.enabled = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.velocity = con.pickupSpeed * (pickupHolder.position - transform.position);
        isPicked = true;
        startGrab = Time.time;
    }
    void Hold()
    {
        Rigidbody rb;
        if (transform.GetComponent<Rigidbody>() == null)
            rb = gameObject.AddComponent<Rigidbody>();
        else rb = transform.GetComponent<Rigidbody>();
        rb.velocity = con.pickupSpeed * (pickupHolder.position - transform.position);
    }
    void LetGo()
    {
        Collider coll = GetComponent<Collider>();
        coll.enabled = true;
        startGrab = 0;
    }
}
