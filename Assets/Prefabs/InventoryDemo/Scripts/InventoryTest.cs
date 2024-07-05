using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryTest : MonoBehaviour
{
    private Inventory _inventory;

    [SerializeField] private InventoryItem Item1;
    [SerializeField] private InventoryItem Item2;
    [SerializeField] private int amountToChange1;
    [SerializeField] private int amountToChange2;
    void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }
    void Start()
    {
        StartCoroutine(RunInventoryTests());
    }
    IEnumerator RunInventoryTests()
    {
        Debug.Log("Initial Inventory State:");
        Debug.Log(_inventory.ToString());

        // Add items to the inventory
        _inventory.AddItem(Item1, amountToChange1);
        _inventory.AddItem(Item2, amountToChange2);

        yield return new WaitForSeconds(1f);

        Debug.Log("Inventory State after adding items:");
        Debug.Log(_inventory.ToString());

        // Remove items from the inventory
        _inventory.RemoveItem(Item1, amountToChange1);
        _inventory.RemoveItem(Item2, amountToChange2);

        yield return new WaitForSeconds(1f);

        Debug.Log("Inventory State after removing items:");
        Debug.Log(_inventory.ToString());
    }
}