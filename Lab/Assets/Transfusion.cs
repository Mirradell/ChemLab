using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfusion : MonoBehaviour
{
    [SerializeField] GameObject Liquid;
    void Start()
    {
        Liquid.GetComponent<Renderer>().material.SetFloat("_FillAmount", 1);
        //Debug.Log(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
