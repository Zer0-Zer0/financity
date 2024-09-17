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

    [SerializeField] KeyCode AbrirInventario = KeyCode.Tab;

    public static bool canStoreAppear = false;

    void Start()
    {
        Inventory.SetActive(false);
        Store.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(AbrirInventario))
        {
            if (canStoreAppear)
            {
                ChangeStoreVisibility();
                Inventory.SetActive(false);
            }
            else
            {
                Inventory.SetActive(!Inventory.activeSelf);
                Store.SetActive(false);
            }
        }
    }

    }

    private void ChangeStoreVisibility() => Store.SetActive(!Store.activeSelf);

    public void ChangeStoreCanAppear() => canStoreAppear = !canStoreAppear;

    public void StoreCanAppear() => canStoreAppear = true;
    public void StoreCanNotAppear() => canStoreAppear = false;
}
