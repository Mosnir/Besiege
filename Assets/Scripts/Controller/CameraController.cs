using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float distance = 10.0f;
    float horizontal = 0.0f;
    float vertical = 0.0f;
    [SerializeField] float hSpeed = 2.0f;
    [SerializeField] float vSpeed = 2.0f;

    GarageManager garageManager;

    GameObject target;

    Camera mainCamera;

    public GameObject Target { set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        garageManager = Toolbox.Instance.Get<GarageManager>();
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!target || !target.activeInHierarchy)
        {
            if(garageManager.getCurrentVehicle() != null)
                target = garageManager.getCurrentVehicle().gameObject;

            return;
        }

        distance -= Input.mouseScrollDelta.y;
        distance = Mathf.Clamp(distance, 2.0f, 50.0f);

        if (Input.GetMouseButton(1))
        {
            vertical += Input.GetAxisRaw("Mouse Y") * vSpeed;
            vertical = Mathf.Clamp(vertical, -85.0f, 85.0f);
            horizontal += Input.GetAxisRaw("Mouse X") * hSpeed;
        }

        Vector3 offset = target.transform.forward;

        offset = Quaternion.AngleAxis(horizontal, Vector3.up) * offset;

        Vector3 right = Vector3.Cross(offset, Vector3.up);

        offset = Quaternion.AngleAxis(vertical, right) * offset;

        offset *= distance;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, target.transform.position - offset, 0.05f);

        mainCamera.transform.LookAt(target.transform);

    }
}
