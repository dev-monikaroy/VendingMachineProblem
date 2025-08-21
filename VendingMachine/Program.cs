using VendingMachineApp;
using System;

class Program
{
    static void Main(string[] args)
    {
        VendingMachine vm = new VendingMachine();
        bool running = true;

        Console.WriteLine("Welcome to the Vending Machine");

        while (running)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1 - Insert Coin (Nickel, Dime, Quarter)");
            Console.WriteLine("2 - Select Product (Cola, Chips, Candy)");
            Console.WriteLine("3 - Check Display");
            Console.WriteLine("4 - Exit");

            Console.Write("Enter choice: ");
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1": // Insert coin
                    Console.Write("Insert coin (Nickel, Dime, Quarter): ");
                    string coinInput = Console.ReadLine()?.Trim();

                    if (Enum.TryParse<CoinType>(coinInput, true, out CoinType coin))
                    {
                        if (coin == CoinType.Penny)
                        {
                            Console.WriteLine("Sorry, Pennies are not accepted.");
                        }
                        else
                        {
                            vm.InsertCoin(coin);
                            Console.WriteLine($"Accepted: {coin}, Current Balance: {vm.CheckDisplay()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid coin type. Try Nickel/Dime/Quarter.");
                    }
                    break;

                case "2": // Select product
                    Console.Write("Select product (Cola, Chips, Candy): ");
                    string product = Console.ReadLine()?.Trim();

                    decimal balanceBefore = vm.GetBalance();
                    decimal price = vm.GetProductPrice(product);

                    if (price == -1) // invalid product
                    {
                        Console.WriteLine("Sorry, we don't have that product currently. Only Cola, Chips, Candy available");
                    }
                    else if (balanceBefore < price)
                    {
                        Console.WriteLine($"PRICE {price:0.00}");
                        Console.WriteLine($"Current balance: {vm.CheckDisplay()}");
                    }
                    else if (balanceBefore > price)
                    {
                        Console.WriteLine($"Balance exceeds price of {product}. EXACT CHANGE ONLY.");
                        Console.WriteLine($"Current balance: {vm.CheckDisplay()}");
                    }
                    else // exact match
                    {
                        vm.SelectProduct(product);
                        Console.WriteLine($"Dispensing {product}...");
                        Console.WriteLine("THANK YOU !!");
                    }
                    break;

                case "3": // Check display
                    Console.WriteLine($"Display: {vm.CheckDisplay()}");
                    break;

                case "4": // Exit
                    running = false;
                    Console.WriteLine("Thank you for using the Vending Machine !!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
