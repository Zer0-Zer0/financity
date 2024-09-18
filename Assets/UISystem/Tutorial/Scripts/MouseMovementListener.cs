using UnityEngine;

public class MouseMovementListener : MonoBehaviour
{
    [SerializeField] private float threshold = 50f;
    [SerializeField] private GameEvent OnMouseMove;
    private Vector2 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 mouseDelta = currentMousePosition - lastMousePosition;

        if (Mathf.Abs(mouseDelta.x) > threshold || Mathf.Abs(mouseDelta.y) > threshold)
            OnMouseMove.Raise(this, null);

        lastMousePosition = currentMousePosition;
    }
}
