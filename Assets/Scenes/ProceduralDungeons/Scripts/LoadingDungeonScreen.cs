using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadingDungeonScreen : MonoBehaviour
{
    [SerializeField] GameObject LoadCanvas;
    void Start()
    {
        OpenScreen();
    }

    private void OpenScreen()
    {
        Time.timeScale = 0f;
        LoadCanvas.SetActive(true);
    }

    public void CloseScreen()
    {
        Time.timeScale = 1f;
        LoadCanvas.SetActive(false);
    }
}
