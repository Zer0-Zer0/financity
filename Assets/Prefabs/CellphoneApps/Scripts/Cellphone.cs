using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone : MonoBehaviour
{
    [SerializeField] Transform AppsParent;
    List<GenericApp> _appsList;

    void Awake()
    {
        //Transfers all children (they NEED to have the GenericApp script)from the given object to a list
        
        foreach (Transform child in AppsParent)
        {
            GenericApp app = child.GetComponent<GenericApp>();
            _appsList.Add(app);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

        //Hides all the app panels
    public void HideAllApps(){
        foreach (GenericApp app in _appsList)
        {
            app.AppPanel.SetActive(false);
        }
    }

}
