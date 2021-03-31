using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    Rigidbody flask;
    public Vector3 mousePos;
    private Vector3 prevMousePos;
    // Start is called before the first frame update
    void Start()
    {
        flask = GetComponent<Rigidbody>();
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
            transform.position += (mousePos - prevMousePos) * Time.deltaTime * 2;
            prevMousePos = mousePos;

        //    if (Input.mouseScrollDelta != new Vector2(0, 0)) // вращение по колесику мыши
                transform.rotation = Quaternion.Euler(0, 0, Input.mouseScrollDelta.y * 3 + transform.rotation.eulerAngles.z);
        }
    }

    private void OnMouseDown()
    {
        prevMousePos = Input.mousePosition;
    }
    private void OnMouseUp()
    {
        prevMousePos = new Vector3();
    }

}
