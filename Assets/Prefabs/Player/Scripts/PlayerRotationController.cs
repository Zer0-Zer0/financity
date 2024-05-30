using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
        [SerializeField] private float rotationSpeed = 100;

    public GameObject playerSpine;

    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void FixedUpdate()
    {
        if(cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            playerSpine.transform.Rotate(0, yaw, 0);
            transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        // Implement your rotation logic here
    }
}
