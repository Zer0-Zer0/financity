using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] Transform target; // The target GameObject to follow
    [SerializeField] float speed = 5f; // Speed at which to follow the target

    void Update()
    {
        if (target != null)
        {
            // Calculate the step size based on speed and time
            float step = speed * Time.deltaTime;

            // Move the GameObject towards the target's position
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
