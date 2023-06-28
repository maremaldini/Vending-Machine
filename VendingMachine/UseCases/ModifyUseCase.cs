using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.UseCases
{
    public class ModifyUseCase : IUseCase
    {
        private readonly IModifications modifications;
        private readonly IBuyView buyView;
        private readonly IEntityFrameworkRepository entityFrameworkRepository;

        public string Name => "modify";

        public string Description => "Make changes to the vending machine.";

        public bool CanExecute => true; // only when user is authentificated !

        public ModifyUseCase(IModifications modifications, IBuyView buyView, IEntityFrameworkRepository entityFrameworkRepository)
        {
            this.modifications = modifications;
            this.buyView = buyView;
            this.entityFrameworkRepository = entityFrameworkRepository;
        }

        public void Execute()
        {
            Console.WriteLine("     add - Add a new product.");
            Console.WriteLine("  delete - Delete a product.");
            Console.WriteLine("    name - Change the name of an existing product.");
            Console.WriteLine("   price - Change the price of an existing product.");
            Console.WriteLine("quantity - Change the quantity of an existing product.");

            Console.WriteLine("\nWhat modification would you like to make?");

            string input = Console.ReadLine();

            switch (input)
            {
                case "add":
                    entityFrameworkRepository.AddProduct(modifications.GetNewName(), modifications.GetNewPrice(), modifications.GetNewQuantity());
                    break;
                case "delete":
                    entityFrameworkRepository.DeleteProduct(buyView.RequestId());
                    break;
                case "name":
                    entityFrameworkRepository.ChangeTheNameOfTheProduct(buyView.RequestId(), modifications.GetNewName());
                    break;
                case "price":
                    entityFrameworkRepository.ChangeThePriceOfTheProduct(buyView.RequestId(), modifications.GetNewPrice());
                    break;
                case "quantity":
                    entityFrameworkRepository.ChangeTheQuantityOfTheProduct(buyView.RequestId(), modifications.GetNewQuantity());
                    break;
                default:
                    throw new CancelationException();
            }
        }
    }
}