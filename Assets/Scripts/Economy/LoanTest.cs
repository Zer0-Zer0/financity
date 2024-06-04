using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LoanTest : MonoBehaviour
{
    void Update()
    {
        Debug.Log(LoanManager.GenerateRandomLoan(LoanData.Type.CompoundInterest).ToString());
    }
}