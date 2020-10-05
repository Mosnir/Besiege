using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorController : MonoBehaviour
{

    List<MyHingeJoint> joints = new List<MyHingeJoint>();

    [SerializeField] KeyCode forward = KeyCode.Z;
    [SerializeField] KeyCode backward = KeyCode.S;
    float velocity = 0.0f;
    [SerializeField] float velocityMax = 10000.0f;
    [SerializeField] float increasedVelocity = 100.0f;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(MyHingeJoint joint in GetComponentsInChildren<MyHingeJoint>())
        {
            joints.Add(joint);
        }
    }

    private void Update()
    {
        foreach (MyHingeJoint joint in joints)
        {
            if (joint.Joint != null)
            {
                JointMotor m = joint.Joint.motor;



                if (Input.GetKey(forward))
                {
                    velocity += increasedVelocity;
                    velocity = Mathf.Clamp(velocity, 0, velocityMax);
                    m.targetVelocity = velocity;
                }

                else if (Input.GetKey(backward))
                {
                    velocity += increasedVelocity;
                    velocity = Mathf.Clamp(velocity, 0, velocityMax);
                    m.targetVelocity = -velocity;
                }
                else
                    m.targetVelocity = 0;



                joint.Joint.motor = m;
            }
        }

        if (Input.GetKeyUp(forward)) velocity = 0;
        if (Input.GetKeyUp(backward)) velocity = 0;
    }

}
