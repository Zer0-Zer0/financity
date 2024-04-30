using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 10f;
    public string groundTag = "Ground";
    public float groundCheckDistance = 0.2f;
    public float lateralSpeed = 3f; // Velocidade lateral editável

    private Rigidbody rb;
    private bool isGrounded;
    private bool isMovementEnabled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isMovementEnabled)
            return;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, LayerMask.GetMask(groundTag));

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (verticalInput != 0)
        {
            Vector3 moveDirection = transform.forward * verticalInput;
            moveDirection.Normalize();
            rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);

            if (horizontalInput != 0)
            {
                transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
            }
        }
        else if (horizontalInput != 0)
        {
            Vector3 moveDirection = transform.right * horizontalInput;
            moveDirection.Normalize(); // Normaliza o vetor de movimento lateral
            rb.MovePosition(transform.position + moveDirection * lateralSpeed * Time.deltaTime); // Usando a velocidade lateral editável
        }
    }

    public void SetMovementEnabled(bool isEnabled)
    {
        isMovementEnabled = isEnabled;
    }
}
