using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellphoneAppManager : MonoBehaviour
{
    [SerializeField] Transform AppsParent;
    List<GenericApp> _appsList;
    public Button MenuButton;

    void Awake()
    {
        MoveAppsToList();
    }

    void MoveAppsToList(){
        //Transfers all children (they NEED to have the GenericApp script)from the given object to a list
        foreach (Transform child in AppsParent)
        {
            GenericApp app = child.GetComponent<GenericApp>();
            _appsList.Add(app);
        }
    }
}
