using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Vector3 offset;

    [Range(0.1f, 1.0f)]
    public float SmoothFactor = 0.1f;

    public bool rotationActive = true;
    public float rotationSpeed = 5.0f;

    public bool lookAtPlayer = false;
    // Start is called before the first frame update 
    public GameObject ball;
    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (rotationActive)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            offset = camTurnAngle * offset;
        }

        Vector3 newPosition = ball.transform.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPosition, SmoothFactor);

        if (lookAtPlayer || rotationActive)
        {
            transform.LookAt(ball.transform);
        }
    }
}
