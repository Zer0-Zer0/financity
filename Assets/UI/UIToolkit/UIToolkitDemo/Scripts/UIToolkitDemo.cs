using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIToolkitDemo : MonoBehaviour
{
    UIDocument _uiDoc;

    Label _plinkCountText;
    int _plinkCount = 0;

    private void OnEnable() {
        _uiDoc = GetComponent<UIDocument>();

        VisualElement _root = _uiDoc.rootVisualElement;
        Button _plink = _root.Q<Button>("plink");
        _plinkCountText = _root.Q<Label>("count");
        _plink.clicked += plink;
    }

    void plink(){
        Debug.Log("Plink");
        _plinkCount ++;
        _plinkCountText.text = $"Plinks {_plinkCount}";
    }
}
