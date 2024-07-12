using UnityEngine;
using UnityEngine.UI;

public class RawImageAnimation : MonoBehaviour
{
    public RectTransform rawImageRect;
    public float speed = 300f;

    private Vector2 startPosition;
    private Vector2 targetPosition;

    private bool isAnimating = false;
    private bool isOpen = false;

    void Start()
    {
        startPosition = rawImageRect.anchoredPosition;
        targetPosition = new Vector2(0f, startPosition.y);
    }

    void Update()
    {
        if (isAnimating)
        {
            float step = speed * Time.deltaTime;
            Vector2 target = isOpen ? targetPosition : startPosition;
            rawImageRect.anchoredPosition = Vector2.MoveTowards(rawImageRect.anchoredPosition, target, step);

            if (rawImageRect.anchoredPosition == target)
            {
                isAnimating = false;
                if (!isOpen)
                {
                    rawImageRect.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StartAnimation()
    {
        isOpen = true;
        isAnimating = true;
        rawImageRect.gameObject.SetActive(true);
    }

    public void ReturnToStartPosition()
    {
        isOpen = false;
        isAnimating = true;
    }
}
