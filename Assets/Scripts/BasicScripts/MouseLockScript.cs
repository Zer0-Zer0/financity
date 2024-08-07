using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLockScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D Empty;

    private bool tabbedIn = false;

    private void Start()
    {
        HideCursor();
    }

    private void OnEnable()
    {
        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void InputLogic()
    {
        bool _hasClicked = Input.GetKeyDown(KeyCode.Mouse0);
        bool _hasPressedTab = Input.GetKeyDown(KeyCode.Tab);
        if (_hasClicked && !tabbedIn)
            HideCursor();
        else if (_hasPressedTab)
            InventoryTabLogic();
    }

    private void InventoryTabLogic()
    {
        tabbedIn = !tabbedIn;
        if (tabbedIn)
            ShowCursor();
        else
            HideCursor();
    }

    private void Update()
    {
        InputLogic();
    }
}
