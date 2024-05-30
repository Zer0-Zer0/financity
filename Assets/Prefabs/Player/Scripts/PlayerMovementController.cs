using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    public float moveX, moveY;

    void Update()
    {
        moveY = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("X", moveX, 0.1f, Time.deltaTime);
        playerAnimator.SetFloat("Y", moveY, 0.1f, Time.deltaTime);
    }
}
