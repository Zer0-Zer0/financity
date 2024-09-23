using UnityEngine;
using UnityEngine.UI;

public class MoveRawImage : MonoBehaviour
{
    public RawImage image;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 1f;
    public KeyCode activationKey = KeyCode.T;
    public MouseVisibilityToggle mouse;

    private bool isMovingUp = false;
    private bool isMovingDown = true;

    private bool trava = false;

    private Vector3 targetPosition = Vector3.zero;

    void Start()
    {
        image.rectTransform.localPosition = startPosition; 
    }

    void Update()
    {
        if (Input.GetKeyDown(activationKey) && gameObject.activeSelf && !trava)
        {
            isMovingUp = !isMovingUp;
            isMovingDown = !isMovingDown;
            //mouse.ToggleMouse();
        }

        if (isMovingUp)
        {
            MoveUp();
        }
        else if (isMovingDown)
        {
            MoveDown();
        }

        if (targetPosition != Vector3.zero)
        {
        if (image.rectTransform.localPosition.y < targetPosition.y)
        {
            image.rectTransform.localPosition += Vector3.up * speed * Time.deltaTime;
            
        } else
        {
            targetPosition = Vector3.zero;
        }
        }
    }

    void MoveUp()
    {
        if (image.rectTransform.localPosition.y < endPosition.y)
        {
            image.rectTransform.localPosition += Vector3.up * speed * Time.deltaTime;
        }
    }

    void MoveDown()
    {
        if (image.rectTransform.localPosition.y > startPosition.y)
        {
            image.rectTransform.localPosition -= Vector3.up * speed * Time.deltaTime;
        }
    }

    public void OpenCellPhone(Vector3 targetposition)
    {
        trava = true;
        targetPosition = targetposition;
        isMovingUp = true;
        isMovingDown = false;
    }

    public void CloseCellPhone()
    {
        trava = false;
        isMovingUp = false;
        isMovingDown = true;
    }
}
