using System;
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

    enum TransactionPosition
    {
        Receiver,
        Sender
    }

    void MakeTransaction(WalletData receiver, float value, Transaction.TransactionType type)
    {
        Transaction transactionToMake = new Transaction(value, _walletData, receiver, type);
        TransactionValidation(transactionToMake);
        transactionToMake.OnTransactionAccepted.AddListener(OnTransactionAcceptedEventHandler);
    }

    TransactionPosition VerifyTransactionPosition(Transaction transaction)
    {
        if (transaction.Sender == _walletData)
        {
            return TransactionPosition.Receiver;
        }
        else if (transaction.Receiver == _walletData)
        {
            return TransactionPosition.Sender;
        }
        else
        {
            throw new Exception("ERROR ON TRANSACTION POSITION VERIFICATION: A Wallet that was neither the sender nor the receiver tried to validate a transaction.");
        }
    }

    public static TransactionPosition TransactionValidation(Transaction transaction)
    {
        VerifySenderMoney(transaction);
        return VerifyTransactionPosition(transaction);
    }

    public static void VerifySenderMoney(Transaction transaction)
    {
        switch (transaction.Type)
        {
            case Transaction.TransactionType.Physical:
                if (transaction.Sender.CurrentPhysicalMoney < transaction.Value)
                {
                    throw new Exception("ERROR VERIFYING SENDER'S MONEY: Tried to make a PHYSICAL money transaction bigger than the value in the sender wallet itself.");
                }
                break;
            case Transaction.TransactionType.Digital:
                if (transaction.Sender.CurrentDigitalMoney < transaction.Value)
                {
                    throw new Exception("ERROR VERIFYING SENDER'S MONEY: Tried to make a DIGITAL money transaction bigger than the value in the sender wallet itself.");
                }
                break;
            default:
                throw new Exception("ERROR VERIFYING SENDER'S MONEY: Impossible transaction type.");
                break;
        }
    }

    void OnTransactionAcceptedEventHandler(Transaction transaction)
    {
        TransactionValidation(transaction);
        switch (transaction.Type)
        {
            case Transaction.TransactionType.Physical:
                transaction.Receiver.CurrentPhysicalMoney += transaction.Value;
                transaction.Sender.CurrentPhysicalMoney -= transaction.Value;
                break;
            case Transaction.TransactionType.Digital:
                transaction.Receiver.CurrentDigitalMoney += transaction.Value;
                transaction.Sender.CurrentDigitalMoney -= transaction.Value;
                break;
            default:
                throw new Exception("ERROR ON ACCEPTED TRANSACTION EVENT HANDLER: Impossible transaction type.");
                break;
        }
        transaction.OnTransactionAccepted.Remove(OnTransactionAcceptedEventHandler);
    }
}