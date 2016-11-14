using System;
using System.Collections.Generic;
using System.IO;
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
            Console.WriteLine("This program will print out receipts of your shopping baskets given an input file! :)");
            string answer;
            InstructionMessage();
            do
            {
                Console.Write("Enter file path: ");
                string inputFilePath = Console.ReadLine();
                var shoppingCart = program.CreateShoppingCart(inputFilePath);
                program.CalculateAndPrintReceipt(shoppingCart);
                Console.Write("Do you need to print another receipts? Please enter \"Y\" or \"N\":");
                answer = Console.ReadLine().ToLower().Trim();
            } while (answer != "n");
            Console.WriteLine("Please take note of your receipts. When you are done, press enter to exit the program. Have a nice day! :)");
            Console.Read();
        }

        public List<List<IGoods>> CreateShoppingCart(string inputFilePath)
        {
            var factory = new GoodsFactory();
            List<List<IGoods>> shoppingCart = new List<List<IGoods>>();
            List<string> categoriesList = new List<string>() { "general", "books", "food", "medical" };
            List<IGoods> temp = new List<IGoods>();
            string[] inputText = null;
            while (inputText == null)
            {
                
                try
                {
                    inputText = File.ReadAllLines(@inputFilePath);
                }
                catch (Exception e)
                {
                    Console.Write("File was not found. Please enter the correct file path: ");
                    inputFilePath = Console.ReadLine();
                }
            }

            foreach (string input in inputText)
            {
                string entry = input.ToLower();
                if (entry.ToLower().Contains("input"))
                {
                    temp = new List<IGoods>();
                    continue;
                }
                if (entry.StartsWith(" "))
                {
                    shoppingCart.Add(temp);
                    continue;
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

                    temp.Add(factory.Create(category, quantity, nameString, price));
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
            shoppingCart.Add(temp);
            return shoppingCart;
        }


        public void CalculateAndPrintReceipt(List<List<IGoods>> cart)
        {
            int i = 0;
            foreach (List<IGoods> basket in cart)
            {
                Console.WriteLine("Receipt {0}: ", ++i);
                double totalSalesTax = 0.0;
                double totalCost = 0;
                foreach (IGoods good in basket)
                {
                    double tax = 0;

                    if (good.Imported && good.GetType().ToString() != typeof(Goods).ToString())
                    {
                        tax += Math.Ceiling(good.Price * .05 * 20) / 20;
                    }
                    else if (good.Imported && good.GetType().ToString() == typeof(Goods).ToString())
                    {
                        tax += Math.Ceiling(good.Price * .15 * 20) / 20;
                    }
                    else if (good.GetType().ToString() == typeof(Goods).ToString())
                    {
                        tax += Math.Ceiling(good.Price * .10 * 20) / 20;
                    }

                    good.Price += tax;
                    if (good.Quantity > 1)
                    {
                        totalCost += good.Price * good.Quantity;
                        totalSalesTax += tax * good.Quantity;
                    }
                    else
                    {
                        totalCost += good.Price;
                        totalSalesTax += tax;
                    }
                    Console.WriteLine(good.Quantity + " " + good.Name + ": " + good.Price.ToString("F"));
                }
                Console.WriteLine("Sales Taxes: " + totalSalesTax.ToString("F") + "\nTotal: " + totalCost.ToString("F") +
                                  "\n");
            }
        }

        private static void InstructionMessage()
        {
            Console.WriteLine("\nPlease enter your shopping basket items into " +
                              "your input file in this format: Category, Quantity, Name of Good at Price");
            Console.WriteLine("Categories include: General, Books, Food, and Medical: Ex. Books 1 Book at 12.49");
            Console.WriteLine("Please edit your input file to match this format " +
                              "and then enter the file path to your input file: Ex. C:\\inputText.txt");
        }
    }
}