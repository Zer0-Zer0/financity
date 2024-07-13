using UnityEngine;

public class MoveImageOnKeyPress : MonoBehaviour
{
    public RectTransform imageToMove;
    public float targetXPosition;
    public float moveSpeed = 5f;
    public float returnSpeed = 2f;
    public float initialXPosition = -435f;

    private bool isMoving = false;

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            isMoving = true;
            MoveImage(targetXPosition, moveSpeed);
        }
        else
        {
            if (isMoving)
            {
                MoveImage(initialXPosition, returnSpeed);
                if (Mathf.Approximately(imageToMove.anchoredPosition.x, initialXPosition))
                {
                    isMoving = false;
                }
            }
        }
    }
    void MoveImage(float targetPosition, float speed)
    {
        float step = speed * Time.deltaTime;
        imageToMove.anchoredPosition = Vector2.MoveTowards(imageToMove.anchoredPosition, new Vector2(targetPosition, imageToMove.anchoredPosition.y), step);
    }
}
