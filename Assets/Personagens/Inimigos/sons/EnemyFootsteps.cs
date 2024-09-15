using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioSource audioSource;  // Referência ao AudioSource
    public AudioClip Footsteps;      // Som de passos
    public float stepInterval = 0.5f; // Intervalo de tempo entre os sons de passos
    private float stepTimer = 0f;

    public Transform objetoMovimento; // Objeto filho que será monitorado
    private Vector3 lastPosition;     // Armazena a última posição do objeto filho
    private bool isPlayingFootstep = false; // Indica se o som está tocando

    void Start()
    {
        // Verifica se o AudioSource foi atribuído
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Inicializa a última posição do objeto
        if (objetoMovimento != null)
        {
            lastPosition = objetoMovimento.position;
        }
    }

    void Update()
    {
        // Verifica se o objeto filho está se movendo
        if (IsMoving())
        {
            stepTimer -= Time.deltaTime;

            // Toca o som de passo a cada intervalo
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval; // Reseta o timer
            }
        }
        else
        {
            // Se o objeto parar de se mover, parar o som
            StopFootstep();
        }
    }

    // Função que toca o som de passos
    void PlayFootstep()
    {
        if (audioSource != null && Footsteps != null)
        {
            if (!isPlayingFootstep)
            {
                audioSource.PlayOneShot(Footsteps);
                isPlayingFootstep = true;
            }
        }
    }

    // Função que para o som de passos
    void StopFootstep()
    {
        if (audioSource != null && isPlayingFootstep)
        {
            audioSource.Stop(); // Para a reprodução do áudio
            isPlayingFootstep = false;
        }
    }

    // Verifica se o objeto filho está se movendo
    bool IsMoving()
    {
        // Compara a posição atual do objeto com a posição anterior
        if (objetoMovimento != null && (objetoMovimento.position != lastPosition))
        {
            // Atualiza a última posição
            lastPosition = objetoMovimento.position;
            return true;
        }

        return false;
    }
}
