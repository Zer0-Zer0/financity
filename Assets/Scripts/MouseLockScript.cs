using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLockScript : MonoBehaviour
{
    private bool _isCursorLocked = true;

    void Start()
    {
        SetCursorState(_isCursorLocked);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _isCursorLocked = !_isCursorLocked;
            SetCursorState(_isCursorLocked);
        }
    }

    void SetCursorState(bool isCursorLocked)
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
