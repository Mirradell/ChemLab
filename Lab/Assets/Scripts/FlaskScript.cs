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
        if (!other.CompareTag("flask")) return;

        if (flask.position.y < other.GetComponent<Rigidbody>().position.y) // считается перелив из верхнего в нижний
            return;

        Debug.Log(other.tag);
        // TODO: сделать переливание из flask в other
        gameObject.GetComponent<Transfusion>().fillingFlask = other.GetComponent<Transfusion>();
        other.GetComponent<Titrovanie>().isFilling = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("flask")) return;

        if (flask.position.y < other.GetComponent<Rigidbody>().position.y) // считается перелив из верхнего в нижний
            return;

        gameObject.GetComponent<Transfusion>().fillingFlask = null;
        other.GetComponent<Titrovanie>().isFilling = false;
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
