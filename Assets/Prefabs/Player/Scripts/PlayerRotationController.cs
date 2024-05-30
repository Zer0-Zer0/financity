using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private Transform wall;
    [SerializeField] private float rotationSpeed = 100;

    void AdjustRotation()
    {
        if (Vector3.Distance(transform.position, wall.position) <= 3.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, wall.rotation, 1f);
        }
    }

    void FixedUpdate()
    {
        // Implement your rotation logic here
    }
}
