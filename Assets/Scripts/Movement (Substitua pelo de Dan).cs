using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;
    
    private Rigidbody rb;
    private bool isGrounded;
    private bool isMovementEnabled = false; // ativa a mov

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isMovementEnabled)
            return; // se tiver ativado comece a trackear

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 newPosition = transform.position + movementDirection * speed * Time.deltaTime;
        rb.MovePosition(newPosition);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // vou usar para ativar ou desativar
    public void SetMovementEnabled(bool isEnabled)
    {
        isMovementEnabled = isEnabled;
    }
}
