using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLockScript : MonoBehaviour
{
    private bool _isCursorLocked = true;

    [SerializeField]
    private Texture2D Empty;

    private void Start()
    {
        Cursor.SetCursor(Empty, new Vector2(0f, 0f), CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.SetCursor(Empty, new Vector2(0f, 0f), CursorMode.Auto);
        }
    }
}
