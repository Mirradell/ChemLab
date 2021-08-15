using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    Rigidbody flask;

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

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (!other.CompareTag("flask")) return;

        if (flask.position.y < other.GetComponent<Rigidbody>().position.y) // считается перелив из верхнего в нижний
            return;

        // TODO: сделать переливание из flask в other
        other.GetComponent<Transfusion>().FillAmount += flask.GetComponent<Transfusion>().liquidAway;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("flask"))
    //    {
    //        flask.position = new Vector3(flask.position.x, 1.7f, flask.position.z);
    //        flask.useGravity = false;
    //    }
    //}
}
