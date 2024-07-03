using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(FinanceManager))]
public class MoneyDeductingManager : MonoBehaviour
{
    private List<string> transactions = new List<string>();
    private float pendingBalanceChange = 0f;
    private float balance = 0f;

    private FinanceManager financeManager;

    void Awake()
    {
        financeManager = GetComponent<FinanceManager>();
    }
}