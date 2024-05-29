using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class JumpingPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator jumpAnimation;

    [SerializeField] private LayerMask layerGround;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float radiusChecker;

    [SerializeField] private float maxHeight;
    [SerializeField] private float timeToMaxHeight;
    private Vector3 _yJumpForce;
    private float _jumpSpeed;
    private float _gravity;
    private float move;
    [SerializeField] private float forwardJumpDistance;



    private void Start()
    {
        SetGravity();
        
    }

    private void Update()
    {
        JumpForce();
       GravityForce();
        move = Input.GetAxis("Vertical");
        
    }

    private void SetGravity()
    {
        _gravity = (2 * maxHeight) / Mathf.Pow(timeToMaxHeight, 2);
        _jumpSpeed = _gravity * timeToMaxHeight;
    }

    private void GravityForce()
    {
        _yJumpForce += _gravity * Time.deltaTime * Vector3.down;
       characterController.Move(_yJumpForce);

        if (IsGrounded() == true) _yJumpForce = Vector3.zero; 
    }

    private void JumpForce()
    {
        
        if (IsGrounded() == true)
          {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Calcula a força do salto vertical
            _yJumpForce = _jumpSpeed * Vector3.up;

            // Calcula a componente horizontal do salto
            Vector3 horizontalJumpForce = Vector3.zero;
            if (move != 0)
            {
                horizontalJumpForce = transform.forward * forwardJumpDistance;
            }

            // Aplica a força resultante ao movimento do personagem
            _yJumpForce += horizontalJumpForce * _jumpSpeed;

            characterController.Move(_yJumpForce);

            // Determina qual animação chamar com base no movimento do personagem
            if (move != 0)
            {
                jumpAnimation.SetBool("puloCorrida", true);
            }
            else
            {
                jumpAnimation.SetBool("jump", true);
            }
        }
    }
    else
    {
        jumpAnimation.SetBool("jump", false);
        jumpAnimation.SetBool("puloCorrida", false);
    }

    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics.CheckSphere(groundChecker.position, radiusChecker, layerGround);
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundChecker.position, radiusChecker);
    }

    
}