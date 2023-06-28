using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine
{
    public class Modifications : IModifications
    {
        public string GetNewName()
        {
            Console.WriteLine("Enter the name of the product: ");

            string name = Console.ReadLine();

            if (name == null || name == "" || string.IsNullOrEmpty(name))
            {
                throw new InvalidTypeException();
            }

            return name;
        }

        public double GetNewPrice()
        {
            Console.WriteLine("Enter the price of the product: ");

            bool worked = double.TryParse(Console.ReadLine(), out double price);

            if (!worked || price < 0 || price > 1000)
            {
                throw new InvalidTypeException();
            }

            return price;
        }

        public int GetNewQuantity()
        {
            Console.Write("Enter the quantity of the product: ");

            bool worked = int.TryParse(Console.ReadLine(), out int quantity);

            if (!worked || quantity < 0 || quantity > 100)
            {
                throw new InvalidTypeException();
            }

            return quantity;
        }
    }
}
