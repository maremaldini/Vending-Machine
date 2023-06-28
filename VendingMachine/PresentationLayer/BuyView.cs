using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories;


namespace iQuest.VendingMachine.PresentationLayer
{
    public class BuyView : DisplayBase, IBuyView
    {
        public int RequestId()
        {
            Display("Enter the ID for the product: ", ConsoleColor.White);

            string id = Console.ReadLine();

            bool worked = Int32.TryParse(id, out int idInt);

            if (worked) 
            {
                DisplayLine($"Item with ID: {idInt} set succsefully!", ConsoleColor.Green);
                return idInt;
            }

            else
            {
                DisplayLine("Invalid ID or product OUT OF STOCK, please try again.", ConsoleColor.Red);
                return RequestId();
            }
        }

        public bool ConfirmPayment(string name)
        {
            Display($"\nYou choosed the product ", ConsoleColor.White);
            Display($"{name}", ConsoleColor.Red);
            DisplayLine(".", ConsoleColor.White);

            Display("\nDo you really want to purchase this item? ", ConsoleColor.White);
            Display("(yes/no) \n", ConsoleColor.DarkCyan);

            string read = Console.ReadLine();

            switch (read)
            {
                case "yes":
                    DisplayLine("\nYou confirmed!\n", ConsoleColor.Green);
                    return true;
                case "no":
                    return false;
                default:
                    Display("\nWrong format!", ConsoleColor.Red);
                    return false;
            }
        }

        public void DispenseProduct(string productName)
        {
            Display($"\nProduct {productName} was dispensed.", ConsoleColor.Green);
        }
    }
}