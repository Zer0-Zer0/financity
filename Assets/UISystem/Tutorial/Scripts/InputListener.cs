using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] KeyCode ListenedInput;
    [SerializeField] InputType inputType;
    [SerializeField] GameEvent OnInputListenedPressed;
    enum InputType
    {
        Normal,
        Down,
        Up
    }

    void Update()
    {
        bool IsListenedInputPressed = false;
        switch (inputType)
        {
            case InputType.Normal:
                IsListenedInputPressed = Input.GetKey(ListenedInput);
                break;
            case InputType.Down:
                IsListenedInputPressed = Input.GetKeyDown(ListenedInput);
                break;
            case InputType.Up:
                IsListenedInputPressed = Input.GetKeyUp(ListenedInput);
                break;
        }

        if (IsListenedInputPressed == true)
            OnInputListenedPressed.Raise(this, ListenedInput);
    }
}
