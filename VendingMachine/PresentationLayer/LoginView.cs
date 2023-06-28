using System;
using iQuest.VendingMachine.PresentationLayer.Interfaces;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class LoginView : DisplayBase, ILogInView
    {
        public string AskForPassword()
        {
            Console.WriteLine();
            Display("Type the admin password: ", ConsoleColor.Cyan);
            return Console.ReadLine();
        }
    }
}