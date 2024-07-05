using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryTest : MonoBehaviour
{
    Inventory _inventory;
    void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }
    void Update()
    {
        Debug.Log(_inventory.ToString());
    }
}