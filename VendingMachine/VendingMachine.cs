using System;
using System.Collections.Generic;

namespace VendingMachineApp
{
    public enum CoinType
    {
        Nickel,   // 0.05
        Dime,     // 0.10
        Quarter,  // 0.25
        Penny     // Invalid
    }

    public class VendingMachine
    {
        private decimal _currentBalance;
        private string _lastMessage;
        public List<CoinType> CoinReturn { get; private set; }
        public Dictionary<string, decimal> Products { get; private set; }

        public VendingMachine()
        {
            _currentBalance = 0m;
            _lastMessage = "INSERT COIN";
            CoinReturn = new List<CoinType>();

            Products = new Dictionary<string, decimal>
            {
                { "Cola", 1.00m },
                { "Chips", 0.50m },
                { "Candy", 0.65m }
            };
        }

        public void InsertCoin(CoinType coin)
        {
            switch (coin)
            {
                case CoinType.Nickel:
                    _currentBalance += 0.05m;
                    break;
                case CoinType.Dime:
                    _currentBalance += 0.10m;
                    break;
                case CoinType.Quarter:
                    _currentBalance += 0.25m;
                    break;
                default:
                    CoinReturn.Add(coin); // Penny rejected
                    break;
            }

            UpdateDisplay();
        }

        public void SelectProduct(string productName)
        {
            if (!Products.ContainsKey(productName))
            {
                _lastMessage = "INVALID PRODUCT";
                return;
            }

            decimal price = Products[productName];

            if (_currentBalance == price)
            {
                _currentBalance -= price; // Deduct price
                _lastMessage = "THANK YOU";

                // After THANK YOU, reset balance
                _currentBalance = 0m;
            }
            else
            {
                _lastMessage = $"PRICE {price:0.00}";
            }
        }

        public string CheckDisplay()
        {
            string message = _lastMessage;

            // Reset display after "THANK YOU" or "PRICE"
            if (_lastMessage == "THANK YOU")
            {
                _lastMessage = "INSERT COIN";
            }
            else if (_lastMessage.StartsWith("PRICE"))
            {
                _lastMessage = _currentBalance > 0 ? _currentBalance.ToString("0.00") : "INSERT COIN";
            }
            else
            {
                _lastMessage = _currentBalance > 0 ? _currentBalance.ToString("0.00") : "INSERT COIN";
            }

            return message;
        }

        private void UpdateDisplay()
        {
            _lastMessage = _currentBalance > 0 ? _currentBalance.ToString("0.00") : "INSERT COIN";
        }

        public decimal GetBalance()
        {
            return _currentBalance;
        }

        public decimal GetProductPrice(string productName)
        {
            if (!Products.ContainsKey(productName))
                return -1; // invalid product
            return Products[productName];
        }

    }
}
