using System.Collections.Generic;
using UnityEngine;

public class StockController : MonoBehaviour
{
    [Header("Stock Settings")]
    [SerializeField] private List<FluctuatingValueSO> stocks; // List of stocks to manage
    [SerializeField] private float updateInterval = 1.0f; // Time interval for updating stocks

    private float updateTimer;

    private void Start()
    {
        // Initialize each stock
        foreach (var stock in stocks)
        {
            stock.Init();
            stock.OnStockUpdate += HandleStockUpdate; // Subscribe to stock update events
        }
    }

    private void Update()
    {
        // Update stocks at specified intervals
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            UpdateStocks();
            updateTimer = 0f; // Reset the timer
        }
    }

    private void UpdateStocks()
    {
        foreach (var stock in stocks)
        {
            stock.Tick(); // Call Tick on each stock to update its value
        }
    }

    private void HandleStockUpdate(float open, float close, float high, float low)
    {
        // Handle stock update event (e.g., log the values, update UI, etc.)
        Debug.Log($"Stock Update: Open: {open}, Close: {close}, High: {high}, Low: {low}");
        
        // Here you can add additional logic, such as updating a UI element or triggering other events
    }
}
