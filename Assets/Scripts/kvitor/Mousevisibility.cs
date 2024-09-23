using UnityEngine;

public class MouseVisibilityToggle : MonoBehaviour
{
    // Variável para controlar o estado do cursor
    private bool isCursorVisible = true;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    public void ToggleMouse()
    {
        isCursorVisible = !isCursorVisible; // Inverte o estado de visibilidade do mouse
        Cursor.visible = isCursorVisible; // Define o cursor como visível ou invisível
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // Bloqueia ou libera o movimento do cursor
    }
}
