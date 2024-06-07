using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator _animator;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _wheelLeft;
    [SerializeField] Transform _wheelRight;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float _calculatedRotSpeed = _rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            _animator.enabled = true;
            _wheelLeft.Rotate(new Vector3(_calculatedRotSpeed, 0, 0));
            _wheelRight.Rotate(new Vector3(_calculatedRotSpeed, 0, 0));
            //_animator.SetBool("mover",true);
        }
        else
        {
            _animator.enabled = false;
            //_animator.SetBool("mover",false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //_animator.enabled=true;
            //_animator.SetBool("mover",true);
            _wheelLeft.Rotate(new Vector3(-_calculatedRotSpeed, 0, 0));
            _wheelRight.Rotate(new Vector3(_calculatedRotSpeed, 0, 0));
            transform.Rotate(0, _calculatedRotSpeed * -1, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //_animator.enabled=true;
            //_animator.SetBool("mover",true);
            _wheelLeft.Rotate(new Vector3(_calculatedRotSpeed, 0, 0));
            _wheelRight.Rotate(new Vector3(-_calculatedRotSpeed, 0, 0));
            transform.Rotate(0, _calculatedRotSpeed, 0);
        }
    }

    
}
