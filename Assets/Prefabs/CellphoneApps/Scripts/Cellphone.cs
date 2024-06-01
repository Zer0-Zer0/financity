using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone : MonoBehaviour
{
    public Transform AppsParent;

    [SerializeField] private Button MenuButton;

    private List<GenericApp> AppsList;

    void Awake()
    {
        //Transfers all children (they NEED to have the GenericApp script)from the given object to a list
        
        foreach (Transform child in AppsParent)
        {
            GenericApp app = child.GetComponent<GenericApp>();
            AppsList.Add(app);
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
        foreach (GenericApp app in AppsList)
        {
            app.AppPanel.SetActive(false);
        }
    }

}
