using UnityEngine;

public class DroneCamera : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow;
    [SerializeField] Transform _objectToLookAt;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _followSpeed = 5f;

    void FixedUpdate()
    {
        if (_objectToFollow == null || _objectToLookAt == null)
        {
            return;
        }

        // Calculate the desired position for the camera to follow the object
        Vector3 targetPosition = _objectToFollow.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);

        // Make the camera look at Fixedthe _objectToLookAt
        transform.LookAt(_objectToLookAt);
    }
}
