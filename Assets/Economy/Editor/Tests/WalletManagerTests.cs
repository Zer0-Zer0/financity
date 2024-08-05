using System;
using UnityEngine;
using NUnit.Framework;

public class WalletManagerTests
{
    private WalletData _walletData;
    private GameObject _testObject;
    private WalletManager _walletManager;

    [SetUp]
    public void Setup()
    {
        _testObject = new GameObject();
        _walletManager = _testObject.AddComponent<WalletManager>();
        _walletData = ScriptableObject.CreateInstance<WalletData>();
        _walletManager.Wallet = _walletData;
    }

    [Test]
    public void TestTransactionValidation()
    {
        // Arrange
        WalletData senderWallet = _walletManager.Wallet;
        _walletManager.Wallet.CurrentPhysicalMoney = 100f;
        WalletData receiverWallet = ScriptableObject.CreateInstance<WalletData>();
        Transaction transaction = new Transaction(50f, senderWallet, receiverWallet, Transaction.TransactionType.Physical);

        // Act
        WalletManager.TransactionPosition position = _walletManager.TransactionValidation(transaction);

        // Assert
        Assert.AreEqual(WalletManager.TransactionPosition.Sender, position);
    }

    [Test]
    public void TestMakeTransaction()
    {
        // Arrange
        _walletManager.Wallet.CurrentDigitalMoney = 200f;
        WalletData receiverWallet = ScriptableObject.CreateInstance<WalletData>();

        // Act
        Assert.That(() => _walletManager.MakeTransaction(300f, receiverWallet, Transaction.TransactionType.Digital), Throws.TypeOf<Exception>());

        Transaction transaction = _walletManager.MakeTransaction(100f, receiverWallet, Transaction.TransactionType.Digital);

        // Assert
        Assert.IsNotNull(transaction);
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up resources or reset state after each test
        _walletData = null;
        GameObject.DestroyImmediate(_testObject);
    }
}
