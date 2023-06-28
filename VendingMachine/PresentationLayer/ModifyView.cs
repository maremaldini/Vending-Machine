using iQuest.VendingMachine.PresentationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer
{
    public class ModifyView : DisplayBase
    {
        public void ChangeOfNameSuccsefully(string oldName, string newName)
        {
            DisplayLine("You changed the name succsefully!", ConsoleColor.Green);
            DisplayLine($"The {oldName}'s name has been updated to {newName}.", ConsoleColor.Green);
        }

        public void ChangeOfIdSuccsefully(string name, int newId)
        {
            DisplayLine("You changed the ID succsefully!", ConsoleColor.Green);
            DisplayLine($"The {name}'s ID has been updated to {newId}.", ConsoleColor.Green);
        }

        public void ChangeOfQuantitySuccsefully(string name , int newQuantity)
        {
            DisplayLine("You added the quantity succsefully!", ConsoleColor.Green);
            DisplayLine($"The {name}'s quantity has been updated to {newQuantity}.", ConsoleColor.Green);
        }

        public void ChangeOfPriceSuccsefully(string name, int newPrice)
        {
            DisplayLine("You changed the price succsefully!", ConsoleColor.Green);
            DisplayLine($"The {name}'s price has been updated to {newPrice}.", ConsoleColor.Green);
        }
    }
}
