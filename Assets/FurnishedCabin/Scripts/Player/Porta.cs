using System.Collections;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public bool isOpen = false;
    public bool isDoubleDoor = false;
    public bool reverseRotation = false;
    public Porta outraPorta;
    public GameObject player;
    public float interactionDistance = 2f;
    public AudioClip somAbrindo;
    public AudioClip somFechando;

    private float closedRotationY = 0f;
    private float openRotationY = 88f;
    private float rotationSpeed = 90f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (isOpen)
            StartCoroutine(RotacionarPorta(openRotationY));
        else
            StartCoroutine(RotacionarPorta(closedRotationY));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear())
        {
            if (!isOpen)
                Abrir();
            else
                Fechar();
        }
    }

    public void Abrir()
    {
        if (!isOpen)
        {
            isOpen = true;

            if (isDoubleDoor && outraPorta != null)
                outraPorta.Abrir();

            if (!reverseRotation)
                StartCoroutine(RotacionarPorta(openRotationY));
            else
                StartCoroutine(RotacionarPorta(-openRotationY)); // Rotação reversa

            if (somAbrindo != null)
            {
                audioSource.PlayOneShot(somAbrindo);
            }
        }
    }

    public void Fechar()
    {
        if (isOpen)
        {
            isOpen = false;

            if (isDoubleDoor && outraPorta != null)
                outraPorta.Fechar();

            if (!reverseRotation)
                StartCoroutine(RotacionarPorta(closedRotationY));
            else
                StartCoroutine(RotacionarPorta(-closedRotationY));

            if (somFechando != null)
            {
                audioSource.PlayOneShot(somFechando);
            }
        }
    }

    private IEnumerator RotacionarPorta(float targetRotationY)
    {
        float currentRotationY = transform.localEulerAngles.y;
        while (Mathf.Abs(currentRotationY - targetRotationY) > 0.01f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            currentRotationY = Mathf.MoveTowardsAngle(currentRotationY, targetRotationY, rotationStep);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, currentRotationY, transform.localEulerAngles.z);
            yield return null;
        }
    }

    private bool IsPlayerNear()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= interactionDistance;
    }
}
