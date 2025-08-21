using NUnit.Framework;
using VendingMachineApp;
using System.Linq;

namespace VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTests
    {
        private VendingMachine vm;

        [SetUp]
        public void Setup()
        {
            vm = new VendingMachine();
        }

        [Test]
        public void Display_Should_Show_InsertCoin_When_NoCoinsInserted()
        {
            Assert.AreEqual("INSERT COIN", vm.CheckDisplay());
        }

        [TestCase(CoinType.Nickel, "0.05")]
        [TestCase(CoinType.Dime, "0.10")]
        [TestCase(CoinType.Quarter, "0.25")]
        public void AcceptCoin_Should_UpdateBalance_ForValidCoins(CoinType coin, string expectedDisplay)
        {
            vm.InsertCoin(coin);
            Assert.AreEqual(expectedDisplay, vm.CheckDisplay());
        }

        [Test]
        public void AcceptCoin_Should_RejectPenny()
        {
            vm.InsertCoin(CoinType.Penny);
            Assert.AreEqual(1, vm.CoinReturn.Count);
            Assert.AreEqual("INSERT COIN", vm.CheckDisplay());
        }

        [Test]
        public void SelectProduct_Should_Dispense_When_EnoughBalance()
        {
            vm.InsertCoin(CoinType.Quarter);
            vm.InsertCoin(CoinType.Quarter);

            vm.SelectProduct("Chips");
            Assert.AreEqual("THANK YOU", vm.CheckDisplay());
            Assert.AreEqual("INSERT COIN", vm.CheckDisplay());
        }

        [Test]
        public void SelectProduct_Should_ShowPrice_When_InsufficientBalance()
        {
            vm.InsertCoin(CoinType.Quarter);
            vm.SelectProduct("Candy");

            Assert.AreEqual("PRICE 0.65", vm.CheckDisplay());
            Assert.AreEqual("0.25", vm.CheckDisplay());
        }

        [Test]
        public void SelectProduct_Should_Reject_When_BalanceExceedsPrice()
        {
            vm.InsertCoin(CoinType.Quarter);
            vm.InsertCoin(CoinType.Quarter);
            vm.InsertCoin(CoinType.Quarter);
            vm.InsertCoin(CoinType.Quarter);

            vm.SelectProduct("Chips");

            Assert.AreEqual("PRICE 0.50", vm.CheckDisplay()); 
            Assert.AreEqual("1.00", vm.CheckDisplay());        
        }

        [Test]
        public void SelectProduct_Should_Reject_InvalidProduct()
        {
            vm.InsertCoin(CoinType.Quarter);
            vm.SelectProduct("Water");

            Assert.AreEqual("INVALID PRODUCT", vm.CheckDisplay());
        }
    }
}
