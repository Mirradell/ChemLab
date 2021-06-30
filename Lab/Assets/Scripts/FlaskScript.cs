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
}
