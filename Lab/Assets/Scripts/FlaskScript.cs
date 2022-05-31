using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    GameObject waterChild;
    Rigidbody flask;
    Material water;

    // Start is called before the first frame update
    void Start()
    {
        flask = GetComponent<Rigidbody>();
        waterChild = transform.GetChild(1).gameObject; // столб воды
        water = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        water.SetFloat("_FillAmount", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flask"))
        {
            if (flask.position.y < other.GetComponent<Rigidbody>().position.y) // считается перелив из верхнего в нижний
                return;

            //проверка на наличие воды
            waterChild.GetComponent<Kinematics>().SetTarget(other.transform.position);
            waterChild.GetComponent<Kinematics>().SetWater(water);
            waterChild.GetComponent<Kinematics>().Solve();
            waterChild.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("flask")) return;

        if (flask.position.y < other.GetComponent<Rigidbody>().position.y) // считается перелив из верхнего в нижний
            return;

       // Debug.Log(other.tag);
        // TODO: сделать переливание из flask в other
        if (other.GetComponent<Titrovanie>() != null && gameObject.GetComponent<Transfusion>() != null)
        {
            gameObject.GetComponent<Transfusion>().fillingFlask = other.GetComponent<Transfusion>();
            other.GetComponent<Titrovanie>().isFilling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("flask"))
        {

            if (flask.position.y < other.GetComponent<Rigidbody>().position.y) // считается перелив из верхнего в нижний
                return;

            if (other.GetComponent<Titrovanie>() != null && gameObject.GetComponent<Transfusion>() != null)
            {
                gameObject.GetComponent<Transfusion>().fillingFlask = null;
                other.GetComponent<Titrovanie>().isFilling = false;
            }
            waterChild.SetActive(false);
        }
        else if (other.CompareTag("drop") && gameObject.GetComponent<Titrovanie>() != null )
        {
            gameObject.GetComponent<Titrovanie>().Drop();
          //  Destroy(other.gameObject);
        }
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
