using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public PlayerController con;
    public Transform player;
    public Transform camera;
    bool isNear;

    private void Update()
    {

        if (con == null)
            con = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        if (player == null)
            player = con.transform;
        if (camera == null)
            camera = GameObject.Find("Camera Holder").transform;

        if (Vector3.Distance(player.position, this.transform.position) <= con.pickDistance)
            isNear = true;
        else isNear = false;
        if (isNear)
            Pick();

    }
    void Pick()
    {
        if (con.autoPickWhenNear)
        {
            Debug.Log("Picked");
        }
        else
        {
            if (Input.GetKey(con.pickupKeybind))
            {
                RaycastHit hit;
                if (!Physics.SphereCast(camera.transform.position, con.pickupRadius, camera.forward, out hit, con.pickDistance, con.pickable))
                {
                    Debug.Log("Picked");
                }
            }
        }
    }
}
