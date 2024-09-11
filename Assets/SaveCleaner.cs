using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCleaner : MonoBehaviour
{
    void OnEnable()
    {
        DataManager.ClearPlayerData();
    }
}
