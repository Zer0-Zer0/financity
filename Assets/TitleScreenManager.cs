using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnAnyKeyPress;
    [SerializeField] GameObject TitlePanel;
    void Update()
    {
        if (Input.anyKey)
        {
            TitlePanel.SetActive(false);
            OnAnyKeyPress?.Invoke();
        }
    }
}
