using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class CellphoneAppManager : MonoBehaviour
{
    [System.Serializable]
    readonly struct GenericApp{
        [SerializeField] readonly internal Button _appButton;
        [SerializeField] readonly GameObject _appPanel;

        internal readonly void ShowAppPanel(){
            _appPanel.SetActive(true);
        }

        internal readonly void HideAppPanel(){
            _appPanel.SetActive(false);
        }
    }

    [SerializeField] readonly List<GenericApp> _appsList;
    [SerializeField] readonly Button MenuButton;

    void Awake()
    {
        SubscribeAppsToEvents();
    }

    ///<summary>
    ///Subscribes the functions from the struct GenericApp to Button's events
    ///</summary>
    void SubscribeAppsToEvents(){
        foreach (GenericApp app in _appsList)
        {
            app._appButton.onClick.AddListener(app.ShowAppPanel);
            MenuButton.onClick.AddListener(app.HideAppPanel);
        }
    }
}