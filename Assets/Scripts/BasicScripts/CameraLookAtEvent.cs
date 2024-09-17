using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraLookAtEvent : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] float detectionAngle = 90f;
    [SerializeField] UnityEvent onLookAt = new UnityEvent();
    [SerializeField] UnityEvent onLookAway = new UnityEvent();

    private bool isLookingAt = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 directionToObject = (transform.position - mainCamera.transform.position).normalized;
        Vector3 cameraForward = mainCamera.transform.forward;

        float angle = Vector3.Angle(cameraForward, directionToObject);

        if (!isLookingAt && angle < detectionAngle)
        {
            isLookingAt = true;
            onLookAt?.Invoke();
        }
        else if (isLookingAt)
        {
            isLookingAt = false;
            onLookAway?.Invoke();
        }
    }
}
