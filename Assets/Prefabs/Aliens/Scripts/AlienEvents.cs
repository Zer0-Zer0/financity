using UnityEngine;
using System.Collections.Generic;

public class AlienEvents : MonoBehaviour
{
    [SerializeField] List<AudioClip> Footsteps;
    [SerializeField] AudioSource audioSource;
    public void PlayFootstepSound()
    {
        AudioClip randomAudioClip = Footsteps[Random.Range(0, Footsteps.Count - 1)];
        audioSource.PlayOneShot(randomAudioClip);
    }
}