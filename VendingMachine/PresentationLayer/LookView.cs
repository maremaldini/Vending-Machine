using System;
using System.Linq;
using System.Collections.Generic;
using iQuest.VendingMachine.PresentationLayer.Interfaces;


namespace iQuest.VendingMachine.PresentationLayer
{
    public class LookView : DisplayBase, ILookView
    {
        public void DisplayProducts(IEnumerable<Product> list)
        {
            if (list == null || !list.Any())
            {
                Display("The vending machine is EMPTY!", ConsoleColor.Red);
                return;
            }

            foreach (Product product in list)
            {
                Display(" Product with ID: ", ConsoleColor.White);
                Display($"{product.Id}", ConsoleColor.Green);
                Display(" Name: ", ConsoleColor.White);
                Display($"{product.Name}", ConsoleColor.Green);
                Display(" Price: ", ConsoleColor.White);
                Display($"{product.Price}", ConsoleColor.Green);
                Display(" Quantity: ", ConsoleColor.White);
                Display($"{product.Quantity}\n", ConsoleColor.Green);
            }
        }
    }
}