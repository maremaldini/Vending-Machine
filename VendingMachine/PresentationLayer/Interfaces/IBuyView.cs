using System;
using System.Collections.Generic;


namespace iQuest.VendingMachine.PresentationLayer.Interfaces
{
    public interface IBuyView
    {
        int RequestId();

        bool ConfirmPayment(string name);

        void DispenseProduct(string name);
    }
}