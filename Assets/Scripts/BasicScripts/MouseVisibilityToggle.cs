using UnityEngine;

public class MouseVisibilityToggle : MonoBehaviour
{
    private bool cursorVisible = true;

    public PauseManager pause;

    void Start()
    {
        ToggleMouse();
    }
    public void ToggleMouse()
    {
        cursorVisible = !cursorVisible;
    }

    void Update()
    {
        if (!pause.isPaused)
        {
            Cursor.visible = cursorVisible;

            if (!cursorVisible)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
}
