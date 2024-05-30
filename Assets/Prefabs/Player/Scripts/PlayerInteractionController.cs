using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Collider itemPickupAreaCollider;
    private bool isInsidePickupArea = false;
    private bool pickupTriggerActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other == itemPickupAreaCollider)
        {
            isInsidePickupArea = true;
        }

        if (other.gameObject.CompareTag("box"))
        {
            playerAnimator.SetTrigger("Death");
            // Handle player death logic
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == itemPickupAreaCollider)
        {
            isInsidePickupArea = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInsidePickupArea)
        {
            pickupTriggerActivated = true;
            playerAnimator.SetTrigger("Pickup");
        }
    }
}
