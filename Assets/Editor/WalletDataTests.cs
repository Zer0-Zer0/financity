using NUnit.Framework;
using UnityEngine;

public class WalletDataTests
{
    private WalletData _walletData;

    [SetUp]
    public void Setup()
    {
        _walletData = ScriptableObject.CreateInstance<WalletData>();
    }

    [Test]
    public void TestInitialValues()
    {
        Assert.AreEqual(800f, _walletData.CurrentDigitalMoney);
        Assert.AreEqual(0f, _walletData.CurrentPhysicalMoney);
        Assert.AreEqual(800f, _walletData.CurrentDebt);
        Assert.AreEqual(800f, _walletData.CurrentMaxDebt);
    }

    [Test]
    public void TestDigitalMoneyUpdate()
    {
        _walletData.CurrentDigitalMoney = 1000f;
        Assert.AreEqual(1000f, _walletData.CurrentDigitalMoney);
    }

    [Test]
    public void TestPhysicalMoneyUpdate()
    {
        _walletData.CurrentPhysicalMoney = 500f;
        Assert.AreEqual(500f, _walletData.CurrentPhysicalMoney);
    }

    [Test]
    public void TestDebtUpdate()
    {
        _walletData.CurrentMaxDebt = 1500f;
        _walletData.CurrentDebt = 1000f;
        Assert.AreEqual(1000f, _walletData.CurrentDebt);
    }

    [Test]
    public void TestMaxDebtUpdate()
    {
        _walletData.CurrentMaxDebt = 2000f;
        Assert.AreEqual(2000f, _walletData.CurrentMaxDebt);
    }
    
    [TearDown]
    public void Teardown()
    {
        // Clean up resources or reset state after each test
        _walletData = null;
    }
}
