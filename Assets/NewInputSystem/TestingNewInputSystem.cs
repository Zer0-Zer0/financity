using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingNewInputSystem : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float movespeed;
    private Vector2 moveDirection;

    [SerializeField]
    InputActionReference move;

    private void Update() => moveDirection = move.action.ReadValue<Vector2>();

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * movespeed;
    }
}
