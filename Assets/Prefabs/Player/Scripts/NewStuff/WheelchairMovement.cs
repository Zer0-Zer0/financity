using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairMovement : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _wheelLeft;
    [SerializeField] Transform _wheelRight;

    private float _calculatedRotSpeed;

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        _calculatedRotSpeed = _rotationSpeed * Time.deltaTime;

        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        if (moveInput != 0)
        {
            MoveWheels(moveInput);
            _animator.enabled = true;
            //_animator.SetBool("mover", true);
        }
        else
        {
            _animator.enabled = false;
            //_animator.SetBool("mover", false);
        }

        if (rotateInput != 0)
        {
            RotateWheels(rotateInput);
        }
    }

    void MoveWheels(float input)
    {
        _wheelLeft.Rotate(new Vector3(_calculatedRotSpeed * input, 0, 0));
        _wheelRight.Rotate(new Vector3(_calculatedRotSpeed * input, 0, 0));
    }

    void RotateWheels(float input)
    {
        _wheelLeft.Rotate(new Vector3(-_calculatedRotSpeed * input, 0, 0));
        _wheelRight.Rotate(new Vector3(_calculatedRotSpeed * input, 0, 0));
        transform.Rotate(0, _calculatedRotSpeed * input, 0);
    }
}
