using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class CellphoneAppManager : MonoBehaviour
{
    [System.Serializable]
    struct GenericApp{
        [SerializeField] internal Button AppButton;
        [SerializeField] GameObject AppPanel;

        internal void ShowAppPanel(){
            AppPanel.SetActive(true);
        }

        internal void HideAppPanel(){
            AppPanel.SetActive(false);
        }
    }

    [SerializeField] List<GenericApp> _appsList;
    [SerializeField] Button MenuButton;

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
            app.AppButton.onClick.AddListener(app.ShowAppPanel);
            MenuButton.onClick.AddListener(app.HideAppPanel);
        }
    }
}