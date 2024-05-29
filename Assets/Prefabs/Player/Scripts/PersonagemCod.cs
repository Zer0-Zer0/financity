using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class PersonagemCod : MonoBehaviour
{
    public float InputX, InputZ;
    public Vector3 dirMoveDesejada;
    public float velRotDesejada = 0.1f;
    public Animator anim;
    public float Speed;
    public float permiteRotPlayer = 0.3f;
    public Camera cam;
    public float verticalVel;
    public Vector3 moveVector;


    // cinemachine 

    public CinemachineVirtualCamera vcam;
    public float[] posCam;
    public int id;
    public CinemachineFramingTransposer composer;

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;

        composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        composer.m_CameraDistance = posCam[id];

    }

    void Update()
    {
        InputMagnitude  ();

        if (Input.GetButtonDown("CameraAjust"))
        {
            AjusteCamera();
            composer.m_CameraDistance = posCam[id];
        }
    }

    void AjusteCamera()
    {
        if (Input.GetButtonDown("CameraAjust") && id < 2)
        {
            id++;
        }
        else if(Input.GetButtonDown("CameraAjust") && id > 1)
        {
            id=0;
        }
    }


    void PlayerMoveRot()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 frente = cam.transform.forward;
        Vector3 direita = cam.transform.right;

        frente.Normalize();
        direita.Normalize();

        dirMoveDesejada = frente * InputZ + direita * InputX;

        // Calcula a rotação desejada apenas ao redor do eixo Y
        Quaternion targetRotation = Quaternion.LookRotation(dirMoveDesejada, Vector3.up);

        // Mantém a rotação atual nos eixos X e Z
        targetRotation.x = transform.rotation.x;
        targetRotation.z = transform.rotation.z;

        // Aplica a rotação desejada
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, velRotDesejada);
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis ("Horizontal");
        InputZ = Input.GetAxis ("Vertical");

        anim.SetFloat("Z", InputZ, 0.0f, Time.deltaTime * 2);
        anim.SetFloat("X", InputX, 0.0f, Time.deltaTime * 2);

        Speed = new Vector2 (InputX, InputZ).sqrMagnitude;

        if (Speed > permiteRotPlayer)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
            PlayerMoveRot();
        }
        else if (Speed < permiteRotPlayer)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
        }
    }
}
