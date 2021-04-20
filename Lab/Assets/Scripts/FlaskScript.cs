using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    Rigidbody flask;
    //private CharacterController character;
    public Vector3 mousePos;
    private Vector3 prevMousePos;
    // Start is called before the first frame update
    void Start()
    {
        flask = GetComponent<Rigidbody>();
//        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        if (prevMousePos != new Vector3())
        {
            mousePos = Input.mousePosition;
            var difference = (mousePos - prevMousePos) * Time.deltaTime;// * 2;
            flask.transform.localPosition = new Vector3(flask.transform.localPosition.x + difference.x,
                                                   flask.transform.localPosition.y + difference.y,
                                                   0f);
            prevMousePos = mousePos;

            flask.transform.rotation = Quaternion.Euler(0, 0, Input.mouseScrollDelta.y * 3 + transform.rotation.eulerAngles.z);
        }
    }

    private void OnMouseDown()
    {
        prevMousePos = Input.mousePosition;
        flask.useGravity = false;
    }
    private void OnMouseUp()
    {
        prevMousePos = new Vector3();
        flask.useGravity = true;
    }

}
