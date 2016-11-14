using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTaxCSharp.Exceptions;
using SalesTaxCSharp.Models;
using SalesTaxCSharp.Models.Factories;

namespace SalesTaxCSharp
{
    public class PrintReceipt
    {

        public static void Main(string[] args)
        {
            var program = new PrintReceipt();
            Console.WriteLine("This program will print out a receipt of your shopping basket!");
            string answer;
            do
            {
                var shoppingCart = program.CreateShoppingCart();
                program.CalculateAndPrintReceipt(shoppingCart);
                Console.Write("Do you need to print another receipt? Please enter \"Y\" or \"N\":");
                answer = Console.ReadLine().ToLower().Trim();
            } while (answer != "n"); 
            Console.WriteLine("Please take note of your receipts. When you are done, press enter to exit the program. Have a nice day! :)");
            Console.Read();
        }

        public List<IGoods> CreateShoppingCart()
        {
            InstructionMessage();
            var factory = new GoodsFactory();
            List<IGoods> shoppingCart = new List<IGoods>();
            List<string> categoriesList = new List<string>() {"general", "books", "food", "medical"};
            while (true)
            {
                string entry = Console.ReadLine().ToLower();

                if (entry == "")
                {
                    break;
                }
                try
                {
                    string separator = "at";
                    int split = entry.LastIndexOf(separator);
                    string info = entry.Substring(0, split);
                    string priceString = entry.Substring(split).Remove(0, 3);                               
                    int quantityPosition = info.IndexOf(' ') + 1;
                    string category = info.Substring(0, quantityPosition - 1);
                    if (!categoriesList.Contains(category))
                    {
                        throw new InvalidCategoryException("Invalid Category, please refer to the categories provided and try again.");
                    }
                    int quantity = Int32.Parse(info.Substring(quantityPosition, 1));
                    string nameString = info.Substring(quantityPosition + 1).TrimStart();
                    double price = Double.Parse(priceString);

                    shoppingCart.Add(factory.Create(category, quantity, nameString, price));
                }
                catch (Exception e)
                {
                    if (e is InvalidCategoryException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry, please refer to the correct format above.");
                    }

                }

            }
            return shoppingCart;
        }


        public void CalculateAndPrintReceipt(List<IGoods> cart)
        {
            Console.WriteLine("Your Receipt: ");
            double totalSalesTax = 0.0;
            double totalCost = 0;
            foreach (IGoods good in cart)
            {
                double tax = 0;

                if (good.Imported && good.GetType().ToString() != typeof(Goods).ToString())
                {
                    tax += Math.Ceiling(good.Price * .05 * 20)/20;            
                } else if (good.Imported && good.GetType().ToString() == typeof(Goods).ToString())
                {
                    tax += Math.Ceiling(good.Price * .15 * 20)/20;                  
                } else if (good.GetType().ToString() == typeof(Goods).ToString())
                {
                    tax += Math.Ceiling(good.Price * .10 * 20)/20;  
                }

                good.Price += tax;
                if (good.Quantity > 1)
                {
                    totalCost += good.Price*good.Quantity;
                    totalSalesTax += tax*good.Quantity;
                }
                else
                {
                    totalCost += good.Price;
                    totalSalesTax += tax;
                }
                Console.WriteLine(good.Quantity + " " + good.Name + ": " + good.Price.ToString("F"));
            }
            Console.WriteLine("Sales Taxes: " + totalSalesTax.ToString("F") + "\nTotal: " + totalCost.ToString("F") + "\n");
        }

        private void InstructionMessage()
        {          
            Console.WriteLine("\nPlease enter your shopping basket items in this format: Category, Quantity, Name of Good at Price");
            Console.WriteLine("Categories include: General, Books, Food, and Medical\n");
            Console.WriteLine("Ex. Books 1 Book at 12.49");
            Console.WriteLine("Enter your items and press enter when you are finished. " +
                              "Press enter once again when you have entered your entire basket to print out your receipt:");
        }
    }
}
 
