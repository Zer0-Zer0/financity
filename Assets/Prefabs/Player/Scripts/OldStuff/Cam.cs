using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform HeadCam;
    public Transform[] pos;
    public int id;
    public Vector3 vel = Vector3.zero;
    private RaycastHit hit;
    public Transform player;

    void Start()
    {
        id = 0;
    }

    void Update()
    {
        AjusteCamera();
    }

    void AjusteCamera()
    {
        if (Input.GetKeyDown(KeyCode.V) && id < 2)
        {
            id++;
        }
        else if (Input.GetKeyDown(KeyCode.V) && id > 1)
        {
            id = 0;
        }
    }

    void LateUpdate()
    {
        transform.LookAt(HeadCam);

        if (!Physics.Linecast(HeadCam.position, pos[id].position))
        {
            transform.position = Vector3.SmoothDamp(transform.position, pos[id].position, ref vel, 0.4f);
        }
        else if (Physics.Linecast(HeadCam.position, pos[id].position, out hit))
        {
            transform.position = Vector3.SmoothDamp(transform.position, hit.point, ref vel, 0.4f);
        }
    }
}