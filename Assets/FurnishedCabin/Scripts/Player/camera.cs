using JetBrains.Annotations;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2f;
    public float minYAngle = -90f;
    public float maxYAngle = 90f;

    public bool ativo = false;

    private float rotationX = 0f;

    void FixedUpdate()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        if (ativo)
        {
            transform.Rotate(Vector3.up * mouseX);
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y, 0f);
        }
    }

    public void mudarsensi(float sensi)
    {
        sensitivity = sensi;
    }

}
