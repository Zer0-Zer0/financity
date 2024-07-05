using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the wallet data and updates UI elements accordingly.
/// </summary>
public class WalletManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the WalletData scriptable object.
    /// </summary>
    [SerializeField] private WalletData _walletData;

    /// <summary>
    /// Initializes the wallet with initial values and subscribes to events.
    /// </summary>
    void Start()
    {
        // Subscribe to events for updating UI or other systems
        _walletData.OnDigitalMoneyUpdate.AddListener(UpdateDigitalMoneyUI);
        _walletData.OnPhysicalMoneyUpdate.AddListener(UpdatePhysicalMoneyUI);
        _walletData.OnDebtUpdate.AddListener(UpdateDebtUI);
        _walletData.OnMaxDebtUpdate.AddListener(UpdateMaxDebtUI);
    }

    /// <summary>
    /// Updates the UI elements displaying digital money.
    /// </summary>
    void UpdateDigitalMoneyUI(float newValue)
    {
        // Update UI elements displaying digital money
    }

    /// <summary>
    /// Updates the UI elements displaying physical money.
    /// </summary>
    void UpdatePhysicalMoneyUI(float newValue)
    {
        // Update UI elements displaying physical money
    }

    /// <summary>
    /// Updates the UI elements displaying debt.
    /// </summary>
    void UpdateDebtUI(float newValue)
    {
        // Update UI elements displaying debt
    }

    /// <summary>
    /// Updates the UI elements displaying maximum debt.
    /// </summary>
    void UpdateMaxDebtUI(float newValue)
    {
        // Update UI elements displaying maximum debt
    }
}
