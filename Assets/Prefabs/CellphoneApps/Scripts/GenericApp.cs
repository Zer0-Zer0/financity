using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericApp : MonoBehaviour
{
    [SerializeField] CellphoneAppManager _appManager;
    public GameObject AppPanel;

    void Awake(){
        _appManager.MenuButton.onClick.AddListener(HideAppPanel);
    }

    public void ShowAppPanel(){
        AppPanel.SetActive(true);
    }

    public void HideAppPanel(){
        AppPanel.SetActive(false);
    }
}
