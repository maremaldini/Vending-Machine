using System;
using System.Collections.Generic;


namespace iQuest.VendingMachine.PresentationLayer.Interfaces
{
    public interface ILookView
    {
        void DisplayProducts(IEnumerable<Product> list);
    }
}