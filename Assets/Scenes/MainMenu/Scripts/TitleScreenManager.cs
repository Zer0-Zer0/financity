using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnAnyKeyPress;
    [SerializeField] Animator TitleScreenAnimator;
    void Update()
    {
        if (Input.anyKey)
        {
            TitleScreenAnimator.SetBool("Pressed", true);
            OnAnyKeyPress?.Invoke();
        }
    }
}
