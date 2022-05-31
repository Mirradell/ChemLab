using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private const float Speed = 2;
    public Vector3 _target;
    public GameObject baseObj;

    void Start()
    {
        _target = Vector3.Cross(new Vector3(-1, 1, -1.5f), transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (!OnTarget())
        {
            var distance = Time.deltaTime * Speed;
            var direction = _target - transform.position;
            direction.Normalize();
            transform.position += direction*distance;
        }//*/

    }

    private void ResetTarget()
    {
        _target = Random.onUnitSphere * 5;
    }

    private bool OnTarget()
    {
        return Vector3.Distance(transform.position, _target) < 1e-1;
    }
    void  OnTriggerEnter (Collider targetObj) {
        if(targetObj.gameObject.CompareTag("Actor"))
        {
            baseObj.GetComponent<Kinematics>().isInActor = true;
        }
    }
    void OnTriggerExit(Collider targetObj)
    {
        if (targetObj.gameObject.CompareTag("Actor"))
        {
            baseObj.GetComponent<Kinematics>().isInActor = false;
        }
    }

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        var zCoordViewport = Camera.main.WorldToViewportPoint(transform.position).z;
        var viewport = Camera.main.ViewportToWorldPoint(new Vector3(mousePos.x, mousePos.y, zCoordViewport));

        transform.position = new Vector3(viewport.x, viewport.y, transform.position.z + Input.mouseScrollDelta.y);
        //rigidbody.rotation = Quaternion.Euler(0, 0, Input.mouseScrollDelta.y * rotationAngle + rigidbody.rotation.eulerAngles.z);

      //  baseObj.GetComponent<Kinematics>()?.Solve();
    }//*/
}
