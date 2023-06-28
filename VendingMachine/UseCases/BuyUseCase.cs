using System;

using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using System.Linq;
using iQuest.VendingMachine.Authentication.Interfaces;

using iQuest.VendingMachine.Exceptions;

namespace iQuest.VendingMachine.UseCases
{
    public class BuyUseCase : DisplayBase, IUseCase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IEntityFrameworkRepository inMemoryRepository;
        private readonly IBuyView buyView;
        private readonly IPaymentView paymentView;

        public string Name => "buy";

        public string Description => "Purchase an item from the vending machine.";

        public bool CanExecute => inMemoryRepository.GetAllProducts().Any() && !authenticationService.IsUserAuthenticated;

        public BuyUseCase(IAuthenticationService authenticationService, IEntityFrameworkRepository inMemoryRepository, IBuyView buyView, IPaymentView paymentView)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.inMemoryRepository = inMemoryRepository ?? throw new ArgumentNullException(nameof(inMemoryRepository));
            this.buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            this.paymentView = paymentView ?? throw new ArgumentNullException(nameof(paymentView));
        }

        public void Execute()
        {
            int id = buyView.RequestId();
            Product product = inMemoryRepository.GetProductById(id);

            if (buyView.ConfirmPayment(product.Name))
            {
                if (product.Quantity < 1) 
                {
                    throw new OutOfStockException();
                }

                switch (paymentView.AskForPaymentMethod())
                {
                    case "cash":
                        paymentView.PayWithCash(id, product.Price, product.Name);
                        break;

                    case "card":
                        paymentView.PrintYourCard();
                        paymentView.PayWithCard(id);
                        break;

                    default:
                        throw new InvalidTypeException();
                }
            }

            else
            {
                throw new CancelationException();
            }

            inMemoryRepository.DecrementQuantity(id);
            buyView.DispenseProduct(inMemoryRepository.GetProductById(id).Name);
        }
    }
}