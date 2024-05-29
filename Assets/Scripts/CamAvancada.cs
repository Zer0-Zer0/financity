using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAvancada : MonoBehaviour
{

    public Vector3 cameraMoveVel = Vector3.zero;
    public GameObject segueObj;
    public float limiteAng = 65.0f;
    public float inputSensit = 155.0f;
    public float mouseX, mouseY;
    public float rotY = 0.0f, rotX = 0.0f;
    public Vector3 rot;
    private Quaternion localRot;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Atualizacao();
    }

    void Init()
    {
        rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    void Atualizacao()
    {
        mouseX = Input.GetAxis("CameraRot");
        mouseY = Input.GetAxis("CameraRot2");

        rotY = mouseX *inputSensit * Time.deltaTime;
        rotX = mouseY * inputSensit * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -limiteAng, limiteAng);
        localRot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRot;

    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, segueObj.transform.position, ref cameraMoveVel, 0.1f);
    }
}
