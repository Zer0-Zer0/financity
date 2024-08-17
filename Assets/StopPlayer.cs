using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class StopPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void IdlePlayer(){
        animator.SetBool("Run", false);
        animator.SetBool("Vault", false);
        animator.SetBool("RunJump", false);
        animator.SetBool("Aim", false);
        animator.SetBool("Fire", false);
        animator.SetBool("Reload", false);
    }
}
