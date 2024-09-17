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

    [SerializeField] GameEvent TimeStop;
    [SerializeField] GameEvent TimeStart;

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

        PauseCheck();
    }

    private void PauseCheck()
    {
        if (Inventory.activeSelf || Store.activeSelf)
        {
            TimeStop.Raise(this, null);
            Time.timeScale = 0f;
        }
        else if (Time.timeScale != 1f)
        {
            TimeStart.Raise(this, null);
            Time.timeScale = 1f;
        }
    }

    private void ChangeStoreVisibility() => Store.SetActive(!Store.activeSelf);

    public void ChangeStoreCanAppear() => canStoreAppear = !canStoreAppear;

    public void StoreCanAppear() => canStoreAppear = true;
    public void StoreCanNotAppear() => canStoreAppear = false;
}
