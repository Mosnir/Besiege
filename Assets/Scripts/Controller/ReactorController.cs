using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorController : MonoBehaviour
{


    [SerializeField] KeyCode key = KeyCode.Space;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (Input.GetKey(key))
        {
            rb.AddForce(-transform.forward * 1000.0f,ForceMode.Acceleration);
        }
    }

}
