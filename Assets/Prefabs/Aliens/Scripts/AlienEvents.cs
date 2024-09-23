using UnityEngine;
using System.Collections.Generic;

public class AlienEvents : MonoBehaviour
{
    [SerializeField] List<AudioClip> Footsteps;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<Component> scripts;
    [SerializeField] List<GameObject> gameObjects;
    public void PlayFootstepSound()
    {
        AudioClip randomAudioClip = Footsteps[Random.Range(0, Footsteps.Count - 1)];
        audioSource.PlayOneShot(randomAudioClip);
    }
    public void KillAlien()
    {
        foreach (var Gameobject in gameObjects)
            Destroy(Gameobject);
        foreach (var script in scripts)
            Destroy(script);

    }
}