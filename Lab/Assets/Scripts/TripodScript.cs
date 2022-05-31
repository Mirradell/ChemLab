using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Biretka"))
        {
            gameObject.GetComponent<Animator>().Play("TripodAnimation");
            var otherRB = other.gameObject.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            Destroy(otherRB.gameObject.GetComponent<MouseFollowingScript>());
            otherRB.position = new Vector3(transform.position.x, transform.GetChild(3).position.y, 2.5f);
            otherRB.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (other.CompareTag("drop"))
            Destroy(other.gameObject);
    }
}
