using UnityEngine;

public class MirrorPosition : MonoBehaviour
{
    public Transform target;

    void FixedUpdate()
    {
        if (target != null)
            transform.position = target.position;
    }
}/*
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MirrorPosition : MonoBehaviour
{
    [SerializeField] Transform target;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        // Calculate your desired position however you like.
        Vector3 desiredPosition = target.position;

        // What velocity gets us there in one timestep?
        Vector3 desiredVelocity = (desiredPosition - transform.position) / Time.deltaTime;

        rb.AddForce((desiredVelocity - rb.velocity) / 5f, ForceMode.VelocityChange);
    }
}//*/