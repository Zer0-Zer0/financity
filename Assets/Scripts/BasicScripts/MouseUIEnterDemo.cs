using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUIEnterDemo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // This method is called when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered: " + gameObject.name);
    }

    // This method is called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited: " + gameObject.name);
    }
}
