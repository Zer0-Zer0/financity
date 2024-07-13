using UnityEngine;

public class DestroyIfOffScreen : MonoBehaviour
{
    public float offScreenDuration = 4f;
    private float offScreenTimer = 0f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found.");
        }
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            if (!IsObjectVisible())
            {
                offScreenTimer += Time.deltaTime;
                if (offScreenTimer >= offScreenDuration)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                offScreenTimer = 0f;
            }
        }
    }

    private bool IsObjectVisible()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
