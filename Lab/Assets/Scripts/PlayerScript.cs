﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float step = 0.05f;
    public float pushForce = 6f;
    private Rigidbody player;
    private CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            player.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + step);
        //            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + step);
        if (Input.GetKey(KeyCode.X))
            player.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - step);
        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - step);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;
        if (body != null)
           body.velocity = hit.moveDirection * pushForce;
    }
}
