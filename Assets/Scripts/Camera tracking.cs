using UnityEngine;

public class FollowObjectWithTransition : MonoBehaviour
{
    public Transform target; // declara o player
    public float transitionDuration = 2f; // tempo que leva pra camera buscar o jogador lá
    public float yOffset = 0f; // muda a profundidade aq
    public float zOffset = 0f; // muda a altura aq pô

    private Camera mainCamera;
    private Vector3 initialPosition;
    private bool isTransitioning = false;
    private float transitionStartTime;
    private bool isFollowing = false;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = mainCamera.transform.position;
    }

    void Update()
    {
        if (isFollowing)
        {
            mainCamera.transform.position = GetTargetPosition();
            mainCamera.transform.LookAt(target.position); // a camera vai ficar seekando o player
        }
        else if (isTransitioning)
        {
            float elapsedTime = Time.time - transitionStartTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            mainCamera.transform.position = Vector3.Lerp(initialPosition, GetTargetPosition(), t);
            mainCamera.transform.LookAt(target.position); // a camera vai ficar seekando o player

            if (t >= 1f)
            {
                isTransitioning = false;
                isFollowing = true;
            }
        }
    }

    public void StartCameraTransition() // o botão tem que chamar isso
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
