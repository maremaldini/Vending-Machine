using System.Collections.Generic;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories;
using iQuest.VendingMachine.Repositories.Interfaces;

using iQuest.VendingMachine.UseCases;

namespace iQuest.VendingMachine
{
    internal class Bootstrapper
    {
        public void Run()
        {
            VendingMachineApplication vendingMachineApplication = BuildApplication();
            vendingMachineApplication.Run();
        }

        private static VendingMachineApplication BuildApplication()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            MainView mainView = new MainView();
            LoginView loginView = new LoginView();

            ILookView lookView = new LookView();
            IEntityFrameworkRepository inMemoryRepository = new EntityFrameworkRepository(context);
            IBuyView buyView = new BuyView();
            IPaymentView paymentView = new PaymentView();
            IModifications modifications = new Modifications();
            IAuthenticationService authenticationService = new AuthenticationService();

            IUseCase modify = new ModifyUseCase(modifications, buyView, inMemoryRepository);

            List<IUseCase> useCases = new List<IUseCase>
            {
                new LoginUseCase(authenticationService, loginView),
                new LogoutUseCase(authenticationService),
                new LookUseCase(lookView, inMemoryRepository),
                new BuyUseCase(authenticationService, inMemoryRepository, buyView, paymentView),
                new ModifyUseCase(modifications, buyView, inMemoryRepository)
        };

            return new VendingMachineApplication(useCases, mainView);
        }
    }
}