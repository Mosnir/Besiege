using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHingeJoint : MyJoint
{
    [SerializeField] HingeJoint joint;
    [SerializeField] bool autoJoint = true;
    [SerializeField] float motorForce = 100;

    public HingeJoint Joint { get => joint; set => joint = value; }

    public override void Join()
    {

        if (!autoJoint) return; 

        Ray ray = new Ray(transform.position, transform.forward);

        foreach (RaycastHit hit in Physics.RaycastAll(ray, 0.5f))
        {
            if (hit.collider.CompareTag("In") && GetComponentInParent<Rigidbody>() != hit.transform.GetComponent<Rigidbody>())
            {
                Joint = hit.transform.gameObject.AddComponent<HingeJoint>();

                Joint.useMotor = true;
                Joint.enableCollision = true;


                if(transform.forward == Vector3.right || transform.forward == Vector3.left)
                {
                    Joint.axis = Joint.transform.InverseTransformDirection(new Vector3(1, 0, 0));
                }
                else
                {
                    Joint.axis = Joint.transform.InverseTransformDirection(new Vector3(0, 0, 1));
                }

                Joint.anchor = new Vector3(0, 0, 0);
                Joint.connectedBody = GetComponentInParent<Rigidbody>();

                joint.breakForce = 20000;
                joint.breakTorque = 20000;

                //Motor
                motor = Joint.motor;
                motor.force = motorForce;
                motor.targetVelocity = 0;
                motor.freeSpin = false;
                Joint.motor = motor;

            }
        }
    }
}
