using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryToggler : MonoBehaviour
{
    [SerializeField]
    GameObject Store;

    [SerializeField]
    GameObject Inventory;

    void Start()
    {
        Inventory.SetActive(false);
        Store.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Store.activeSelf == false)
            Inventory.SetActive(!Inventory.activeSelf);

        if (Store.activeSelf == true)
            Inventory.SetActive(false);
    }

    public void ChangeStoreVisibility() => Store.SetActive(!Store.activeSelf);
}
