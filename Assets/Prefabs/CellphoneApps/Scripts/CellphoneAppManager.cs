using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

class CellphoneAppManager : MonoBehaviour
{
    [SerializeField] Transform AppsParent;
    List<GenericApp> _appsList;
    public Button MenuButton;

    void Awake()
    {
        MoveAppsToList();
    }

    ///<summary>
    ///Transfers all children from the given object to the _appsList (they NEED to have the GenericApp script)
    ///</summary>
    void MoveAppsToList(){
        foreach (Transform child in AppsParent)
        {
            GenericApp app = child.GetComponent<GenericApp>();
            _appsList.Add(app);
        }
    }
}