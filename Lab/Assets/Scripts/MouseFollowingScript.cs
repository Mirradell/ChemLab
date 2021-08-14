﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowingScript : MonoBehaviour
{
    public float rotationAngle;

    private Rigidbody rigidbody;
  //  private float zCoordViewport;
    private float zCoordWorld;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        zCoordWorld = rigidbody.position.z;
    }

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        var zCoordViewport = Camera.main.WorldToViewportPoint(rigidbody.position).z;
        var viewport = Camera.main.ViewportToWorldPoint(new Vector3(mousePos.x, mousePos.y, zCoordViewport));

        rigidbody.position = new Vector3(viewport.x, viewport.y, zCoordWorld);
        rigidbody.rotation = Quaternion.Euler(0, 0, Input.mouseScrollDelta.y * rotationAngle + rigidbody.rotation.eulerAngles.z);
    }

    private void OnMouseDown()
    {
        rigidbody.useGravity = false;
    }

    private void OnMouseUp()
    {
        rigidbody.useGravity = true;
    }
}
