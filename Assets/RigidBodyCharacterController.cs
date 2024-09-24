using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyCharacterController : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f; // Speed of the character
    [SerializeField] float walkSpeed = 3f; // Speed of the character
    [SerializeField] GameObject playerCamera; // Reference to the camera

    private Rigidbody rb;
    private Vector3 inputDirection; // Store input direction for FixedUpdate

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement vector based on input
        inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // If there is some input, move the character
        if (inputDirection.magnitude >= 0.1f)
        {
            MoveCharacter(inputDirection);
        }
    }

    private void MoveCharacter(Vector3 inputDirection)
    {
        // Get the camera's forward and right vectors
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Flatten the vectors to ignore the y-axis
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the desired move direction
        Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        // Move the character
        if (Input.GetKey(KeyCode.LeftShift))
            rb.MovePosition(rb.position + moveDirection * runSpeed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + moveDirection * walkSpeed * Time.fixedDeltaTime);
    }
}
