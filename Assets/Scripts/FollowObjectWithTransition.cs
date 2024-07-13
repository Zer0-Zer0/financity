using UnityEngine;

public class FollowObjectWithTransition : MonoBehaviour
{
    public Transform target;
    public float transitionDuration = 4f;
    public float yOffset = 10f; // altura da câmera acima do jogador
    public float zOffset = 20f; // distância da câmera atrás do jogador
    public float smoothTime = 0.7f; // tempo de suavização

    private Camera mainCamera;
    private Vector3 initialPosition;
    private bool isTransitioning = false;
    private float transitionStartTime;
    private bool isFollowing = false;
    private Vector3 velocity = Vector3.zero;
    private Quaternion initialRotation;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = mainCamera.transform.position;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector3 targetPosition = target.position - target.forward * zOffset + Vector3.up * yOffset;
            // smooth
            Vector3 smoothPosition = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref velocity, smoothTime);
            mainCamera.transform.position = smoothPosition;

            // fazer a câmera olhar para o jogador
            mainCamera.transform.LookAt(target);
        }
        else if (isTransitioning)
        {
            float elapsedTime = Time.time - transitionStartTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            mainCamera.transform.position = Vector3.Lerp(initialPosition, GetTargetPosition(), t);

            // seeka o player na transição
            mainCamera.transform.LookAt(target);

            if (t >= 1f)
            {
                isTransitioning = false;
                isFollowing = true;

                // chamando a função de mover do script de movimentação
                SimpleMovement movementScript = target.GetComponent<SimpleMovement>();
                if (movementScript != null)
                {
                    movementScript.SetMovementEnabled(true);
                }
            }
        } else {
            float angleX = Mathf.Sin(Time.time * 0.35f) * 2.3f;
            float angleY = Mathf.Sin(Time.time * 0.35f * 0.5f) * 2.3f * 0.5f;

            Quaternion swingRotationX = Quaternion.Euler(angleX, 0, 0);
            Quaternion swingRotationY = Quaternion.Euler(0, angleY, 0);

            transform.localRotation = initialRotation * swingRotationX * swingRotationY;
        }
    }

    public void StartCameraTransition()
    {
        if (target != null && !isTransitioning && !isFollowing)
        {
            initialPosition = mainCamera.transform.position;
            transitionStartTime = Time.time;
            isTransitioning = true;
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y += yOffset;
        targetPosition.z += zOffset;
        return targetPosition;
    }
}
