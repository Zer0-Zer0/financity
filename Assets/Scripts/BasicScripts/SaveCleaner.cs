using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCleaner : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log($"{gameObject.name}: SaveCleaner is present and is wiping the save");
        DataManager.ClearPlayerData();
    }
}
