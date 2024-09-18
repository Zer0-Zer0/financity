using UnityEngine;
using System.Collections;

public class RandomAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] float minPauseDuration = 4f;
    [SerializeField] float maxPauseDuration = 15f;

    private Vector3 lastPosition;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = audioSource ?? GetComponent<AudioSource>();
        if (audioSource != null) audioSource.spatialBlend = 1f;
        StartCoroutine(TimedRandomAudio());
    }

    void Update()
    {
        if (isPlaying)
            return;

        if (!IsMoving())
        {
            isPlaying = true;
            StartCoroutine(TimedRandomAudio());
        }
    }

    void PlayRandomAudio()
    {
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned!");
            return;
        }

        AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.volume = Random.Range(0.5f, 0.75f);
        audioSource.PlayOneShot(randomClip);
    }

    IEnumerator TimedRandomAudio()
    {
        PlayRandomAudio();
        yield return new WaitForSeconds(Random.Range(minPauseDuration, maxPauseDuration));
        isPlaying = false;
    }

    bool IsMoving()
    {
        if (transform != null && (transform.position != lastPosition))
        {
            lastPosition = transform.position;
            return true;
        }

        return false;
    }
}
