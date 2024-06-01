using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericApp : MonoBehaviour
{
    RectTransform _rect;
    public GameObject AppPanel;

    void Awake(){
        _rect = transform.GetComponent<RectTransform>();
    }

    public void ShowAppPanel(){
        AppPanel.SetActive(true);
    }
}
