using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowingScript : MonoBehaviour
{
    float rotationAngle = 10;

    new Rigidbody rigidbody;
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

        transform.position = new Vector3(viewport.x, viewport.y, zCoordWorld);
        transform.rotation = Quaternion.Euler(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y, Input.mouseScrollDelta.y * rotationAngle + rigidbody.rotation.eulerAngles.z);
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
