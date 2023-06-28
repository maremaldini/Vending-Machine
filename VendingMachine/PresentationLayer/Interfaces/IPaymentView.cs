using System;

using System.Security.Cryptography;

namespace iQuest.VendingMachine.PresentationLayer.Interfaces
{
    public interface IPaymentView
    {
        string AskForPaymentMethod();
        void PayWithCash(int id, double price, string name);
        void PayWithCard(int id);
        void ChangeForPayingWithCash(double change);
        void PrintYourCard();
    }
}

