using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellphoneAppManager : MonoBehaviour
{
    [System.Serializable]
    internal struct GenericApp
    {
        [SerializeField] internal Button _appButton;
        [SerializeField] GameObject _appPanel;

        internal void ShowAppPanel()
        {
            _appPanel.SetActive(true);
        }

        internal void HideAppPanel()
        {
            _appPanel.SetActive(false);
        }
    }

    [SerializeField] List<GenericApp> _appsList;
    [SerializeField] Button _menuButton;

    void Awake()
    {
        SubscribeAppsToEvents();
    }

    ///<summary>
    ///Subscribes the functions from the struct GenericApp to Button's events
    ///</summary>
    void SubscribeAppsToEvents()
    {
        foreach (GenericApp app in _appsList)
        {
            app._appButton.onClick.AddListener(app.ShowAppPanel);
            _menuButton.onClick.AddListener(app.HideAppPanel);
        }
    }
}