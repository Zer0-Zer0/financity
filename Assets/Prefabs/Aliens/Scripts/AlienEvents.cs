using UnityEngine;
using System.Collections.Generic;

public class AlienEvents : MonoBehaviour
{
    [SerializeField] List<AudioClip> Footsteps;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<Component> scripts;
    public void PlayFootstepSound()
    {
        AudioClip randomAudioClip = Footsteps[Random.Range(0, Footsteps.Count - 1)];
        audioSource.PlayOneShot(randomAudioClip);
    }
    public void KillAlien(){
        foreach(var script in scripts)
            Destroy(script);
    }
}