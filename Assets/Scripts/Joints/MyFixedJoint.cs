using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFixedJoint : MyJoint
{

    public override void Join()
    {

        Ray ray = new Ray(transform.position, transform.forward);

        foreach(RaycastHit hit in Physics.RaycastAll(ray, 0.5f))
        {
            if (hit.collider.CompareTag("In") && GetComponentInParent<Rigidbody>() != hit.transform.GetComponent<Rigidbody>())
            {
                var joint = GetComponentInParent<Rigidbody>().gameObject.AddComponent<FixedJoint>();
                joint.connectedAnchor = hit.collider.transform.position;
                joint.connectedBody = hit.transform.GetComponent<Rigidbody>();
                joint.enableCollision = true;
                joint.breakForce = 20000;
                joint.breakTorque = 20000;
            }
        }
    }
}
