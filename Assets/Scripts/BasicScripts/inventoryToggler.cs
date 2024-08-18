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

    public static bool canStoreAppear = false;

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

        if (Input.GetKeyDown(KeyCode.X) && canStoreAppear)
            ChangeStoreVisibility();

        if (Store.activeSelf == true)
            Inventory.SetActive(false);
    }

    private void ChangeStoreVisibility() => Store.SetActive(!Store.activeSelf);

    public void ChangeStoreCanAppear() => canStoreAppear = !canStoreAppear;

    public void StoreCanAppear() => canStoreAppear = true;
    public void StoreCanNotAppear() => canStoreAppear = false;
}
