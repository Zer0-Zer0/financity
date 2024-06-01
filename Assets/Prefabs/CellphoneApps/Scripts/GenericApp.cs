using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericApp : MonoBehaviour
{
    private RectTransform rectTransform;
    public GameObject AppPanel;

    void Awake(){
        rectTransform = transform.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAppPanel(){
        AppPanel.SetActive(true);
    }
}
