using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzleTransform;
    public float shootForce = 700f;

    private bool rightButtonPressed = false;
    private bool wasMoving = false;

    void Update()
    {
        // Shooting logic
        if (Input.GetMouseButtonDown(1))
        {
            rightButtonPressed = true;
            wasMoving = playerAnimator.GetBool("Walk");
            playerAnimator.SetBool("Walk", false);
            playerAnimator.SetBool("ShootMovement", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightButtonPressed = false;
        }

        if (rightButtonPressed && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (!rightButtonPressed && !wasMoving)
        {
            playerAnimator.SetBool("Walk", false);
            playerAnimator.SetBool("ShootMovement", false);
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzleTransform.position, muzzleTransform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(muzzleTransform.forward * shootForce);
    }
}