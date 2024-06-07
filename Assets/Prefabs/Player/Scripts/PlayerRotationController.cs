using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] public GameObject playerSpine;

    [SerializeField] public bool InvertCamera = false;
    [SerializeField] public bool CanCameraMove = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float MaxLookAngle = 50f;

    // Internal Variables
    private float _yaw = 0.0f;
    private float _pitch = 0.0f;

    void FixedUpdate()
    {
        if(CanCameraMove)
        {
            _yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!InvertCamera)
            {
                _pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                _pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            _pitch = Mathf.Clamp(_pitch, -MaxLookAngle, MaxLookAngle);

            playerSpine.transform.Rotate(0, _yaw, 0);
            transform.localEulerAngles = new Vector3(_pitch, 0, 0);
        }

        // Implement your rotation logic here
    }
}
