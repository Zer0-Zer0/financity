using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCleaner : MonoBehaviour
{
    void Start()
    {
        DataManager.ClearPlayerData();
    }
}
