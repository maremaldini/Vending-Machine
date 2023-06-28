using System;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachine.Tests.UseCases.BuyUseCaseTests
{

    [TestClass]
    public class ConstructorTests
    {
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
        public void HavingANullAuthenticationService_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(null, inMemoryRepository.Object, buyView.Object,paymentView.Object );
            });
        }

        [TestMethod]
        public void HavingANullInMemoryRepository_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(authenticationService.Object, null, buyView.Object, paymentView.Object);
            });
        }

        [TestMethod]
        public void HavingANullBuyView_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, null, paymentView.Object);
            });
        }

        [TestMethod]
        public void HavingANullPaymentView_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, null);
            });
        }

        [TestMethod]
        public void HappyFlowBuyUseCase_WhenInitializingTheUseCase_NoExceptionIsThrown()
        {
            Assert.IsNotNull(new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, paymentView.Object));
        }
    }
}

