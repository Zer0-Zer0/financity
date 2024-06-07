using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRotationController))]
public class MouseCapturer : MonoBehaviour
{
    bool _isCursorLocked = true;
    PlayerRotationController _playerRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerRot = GetComponent<PlayerRotationController>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isCursorLocked = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isCursorLocked = true;
        }

        if (_isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        _playerRot.CanCameraMove = _isCursorLocked;
    }
}
