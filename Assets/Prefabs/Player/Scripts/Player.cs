using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private bool isDead = false;
    [SerializeField] private Collider itemPickupAreaCollider; // Collider of the area where the player can pick up the item
    private bool isInsidePickupArea = false;

    private bool isHanging = false;
    private Transform targetRoot;

    private bool trigger;
    public Transform wall;
    bool pickupTriggerActivated = false;

    [SerializeField] private Transform itemToPickup;
    [SerializeField] private Transform hand;

    [SerializeField] private float rotationSpeed = 100;

    public float moveX, moveY;

    private bool rightButtonPressed = false;
    private bool wasMoving = false; 

    // Variables for the shooting system
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzleTransform; // Reference to the shooting pivot
    public float shootForce = 700f;

    void Start()
    {
        targetRoot = null;
        trigger = true;
    }

    void AdjustRotation()
    {
        if (Vector3.Distance(transform.position, wall.position) <= 3.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, wall.rotation, 1f);
        }
    }

    void FixedUpdate()
    {
        if (!targetRoot) return;
        if (isHanging && trigger)
        {
            transform.position = new Vector3(transform.position.x, targetRoot.position.y, targetRoot.position.z);
            trigger = false;
        }
    }

    void Update()
    {
        moveY = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("X", moveX, 0.1f, Time.deltaTime);
        playerAnimator.SetFloat("Y", moveY, 0.1f, Time.deltaTime);

        float move = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Shooting logic
        if (Input.GetMouseButtonDown(1))
        {
            rightButtonPressed = true;
            wasMoving = playerAnimator.GetBool("Walk"); // Store if it was moving
            playerAnimator.SetBool("Walk", false); // Stop movement
            playerAnimator.SetBool("ShootMovement", true); // Activate shooting movement
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightButtonPressed = false;
        }

        if (rightButtonPressed && Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }

        if (!rightButtonPressed && !wasMoving)
        {
            playerAnimator.SetBool("Walk", false);
            playerAnimator.SetBool("ShootMovement", false);
        }

        // Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("Running", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("Running", false);
        }

        if (!isDead && !isHanging)
        {
            rotation *= Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }

        if (isHanging)
        {
            if (rotation > 1)
            {
                playerAnimator.SetBool("HangingRight", true);
            }
            else if (rotation < -1)
            {
                playerAnimator.SetBool("HangingLeft", true);
            }
            else
            {
                playerAnimator.SetBool("HangingRight", false);
                playerAnimator.SetBool("HangingLeft", false);
            }
        }

        if (move != 0)
        {
            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }

        if (isDead && Input.GetKeyDown(KeyCode.Z))
        {
            playerAnimator.SetTrigger("StandUp");
            isDead = false;
        }

        if (isHanging && Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine("Climbing");
        }

        if (Input.GetKeyDown(KeyCode.E) && isInsidePickupArea)
        {
            pickupTriggerActivated = true;
            playerAnimator.SetTrigger("Pickup");
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzleTransform.position, muzzleTransform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(muzzleTransform.forward * shootForce);
    }

    public void Hang(Transform target)
    {
        if (isHanging) return;

        playerAnimator.SetTrigger("Hanging");
        GetComponent<Rigidbody>().isKinematic = true;
        isHanging = true;
        targetRoot = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == itemPickupAreaCollider)
        {
            isInsidePickupArea = true;
        }

        if (other.gameObject.CompareTag("box"))
        {
            playerAnimator.SetTrigger("Death");
            isDead = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == itemPickupAreaCollider)
        {
            isInsidePickupArea = false;
        }
    }

    IEnumerator Climbing()
    {
        playerAnimator.SetTrigger("climbing");
        yield return new WaitForSeconds(3.24f);
        isHanging = false;
        trigger = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerAnimator.SetLookAtWeight(playerAnimator.GetFloat("IK_Value"));
        playerAnimator.SetLookAtPosition(itemToPickup.position);

        if (pickupTriggerActivated && playerAnimator.GetFloat("IK_Value") > 0.9f)
        {
            itemToPickup.parent = hand;
            itemToPickup.localPosition = new Vector3(-0.0003f, 0.1112f, 0.0518f);
            pickupTriggerActivated = false; // Deactivate trigger after picking up the item
        }

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, playerAnimator.GetFloat("IK_Value"));
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, itemToPickup.position);
    }
}
