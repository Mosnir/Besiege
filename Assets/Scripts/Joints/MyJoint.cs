using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyJoint : MonoBehaviour
{

    protected JointMotor motor;

    public JointMotor Motor { get => motor; set => motor = value; }

    public abstract void Join();
}
