using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<AudioClip> Footsteps;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip Reload;
    public void IdlePlayer()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Vault", false);
        animator.SetBool("RumJump", false);
        animator.SetFloat("Mag", 0f);
        animator.SetBool("Aim", false);
        animator.SetBool("Fire", false);
        animator.SetBool("Reload", false);
    }

    public void PlayFootstepSound()
    {
        AudioClip randomAudioClip = Footsteps[Random.Range(0, Footsteps.Count - 1)];
        audioSource.PlayOneShot(randomAudioClip);
    }

    public void PlayReloadSound()
    {
        audioSource.PlayOneShot(Reload);
    }
}
