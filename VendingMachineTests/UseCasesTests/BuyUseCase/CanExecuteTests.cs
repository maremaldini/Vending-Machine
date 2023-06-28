using System;
using System.Collections.Generic;
using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    [TestClass]
	public class CanExecuteTests
	{
        // public BuyUseCase(IAuthenticationService authenticationService, IInMemoryRepository inMemoryRepository, IBuyView buyView, IPaymentView paymentView)

        private Mock<IAuthenticationService> authenticationService;
        private Mock<IEntityFrameworkRepository> inMemoryRepository;
        private Mock<IBuyView> buyView;
        private Mock<IPaymentView> paymentView;

        [TestInitialize]
        public void TestSetup()
		{
            authenticationService = new Mock<IAuthenticationService>();
            inMemoryRepository = new Mock<IEntityFrameworkRepository>();
            buyView = new Mock<IBuyView>();
            paymentView = new Mock<IPaymentView>();
        }

        [TestMethod]
        public void HavingAtLeastOneProductAndTheUserIsNotAdmin_CanExecuteIsTrue()
        {
            var products = new List<Product>()
            {
                new Product(0, "name", 0f, 0)
            };

            inMemoryRepository.Setup(x => x.GetAllProducts()).Returns(products);

            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(false);

            var buyUseCase = new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, paymentView.Object);

            Assert.IsTrue(buyUseCase.CanExecute);

        }

        [TestMethod]
        public void HavingAtLeastOneProductAndTheUserIsAdmin_CanExecuteIsFalse()
        {
            var products = new List<Product>()
            {
                new Product(0, "name", 0f, 0)
            };

            inMemoryRepository.Setup(x => x.GetAllProducts()).Returns(products);

            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(true);

            var buyUseCase = new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, paymentView.Object);

            Assert.IsFalse(buyUseCase.CanExecute);

        }

        [TestMethod]
        public void HavingNoProductsAndTheUserIsAdmin_CanExecuteIsFalse()
        {
            var products = new List<Product>();

            inMemoryRepository.Setup(x => x.GetAllProducts()).Returns(products);

            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(true);

            var buyUseCase = new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, paymentView.Object);

            Assert.IsFalse(buyUseCase.CanExecute);

        }

        [TestMethod]
        public void HavingNoProductsAndTheUserIsNotAdmin_CanExecuteIsFalse()
        {
            var products = new List<Product>();

            inMemoryRepository.Setup(x => x.GetAllProducts()).Returns(products);

            authenticationService.Setup(x => x.IsUserAuthenticated).Returns(false);

            var buyUseCase = new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, paymentView.Object);

            Assert.IsFalse(buyUseCase.CanExecute);
        }
    }
}