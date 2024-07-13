using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaDrain = 10f;
    public float staminaRegen = 5f;
    public CharacterController controller;
    private Camera mainCamera;
    public bool canMove = false;
    public bool canRun = true; // Nova variável para ativar/desativar a corrida

    public bool move = false;
    private float movementDelay = 25f;
    private float elapsedTime = 0f;
    private bool objective1Sent = false;
    private bool objective2Sent = false;

    public ObjectiveSystem objetivos;
    public AnimatedText objtvcentral;

    [System.Serializable]
    public class FootstepSound
    {
        public string tagName;
        public AudioClip audioClip;
    }

    public FootstepSound[] footstepSounds;

    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Camera principal não encontrada na cena!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!objective1Sent && elapsedTime >= 20f)
        {
            ShowText("Objetivo:\nLigue o Rádio!");
            objective1Sent = true;
            objetivos.AddObjective(1, "Ligue o Rádio!");
        }

        if (!objective2Sent && elapsedTime >= 26.5f)
        {
            ShowText("Objetivo:\nAche o seu Telefone!");
            objective2Sent = true;
            objetivos.AddObjective(2, "Ache o seu Telefone!");
        }

        if (!canMove && elapsedTime >= movementDelay)
        {
            canMove = true;
            move = true;
        }
    }

    void FixedUpdate()
    {
        if (move)
        {
            if (mainCamera == null || !canMove)
                return;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 forward = mainCamera.transform.forward;
            Vector3 right = mainCamera.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 movementDirection = (forward * verticalInput + right * horizontalInput).normalized;

            bool isRunning = canRun && Input.GetKey(KeyCode.LeftShift) && stamina > 0f;
            float currentSpeed = isRunning ? runSpeed : speed;

            if (isRunning)
            {
                stamina -= staminaDrain * Time.deltaTime;
                if (stamina < 0f) stamina = 0f;
            }
            else
            {
                stamina += staminaRegen * Time.deltaTime;
                if (stamina > maxStamina) stamina = maxStamina;
            }

            Vector3 movementVelocity = movementDirection * currentSpeed;

            controller.Move(movementVelocity * Time.deltaTime);

            if (movementVelocity == Vector3.zero)
            {
                audioSource.Stop();
            }

            PlayFootstepSound();
        }
    }

    void PlayFootstepSound()
    {
        if (move)
        {
            if (transform.position != lastPosition)
            {
                foreach (FootstepSound footstepSound in footstepSounds)
                {
                    Vector3 spherePosition = transform.position + Vector3.down * controller.height / 2f;
                    Collider[] colliders = Physics.OverlapSphere(spherePosition, 0.1f);

                    foreach (Collider col in colliders)
                    {
                        if (col.CompareTag(footstepSound.tagName))
                        {
                            if (footstepSound.audioClip != null && !audioSource.isPlaying)
                            {
                                audioSource.clip = footstepSound.audioClip;
                                audioSource.Play();
                                return;
                            }
                        }
                    }
                }
            }
            lastPosition = transform.position;
        }
    }

    void ShowText(string text)
    {
        objtvcentral.ShowText(text);
    }
}
