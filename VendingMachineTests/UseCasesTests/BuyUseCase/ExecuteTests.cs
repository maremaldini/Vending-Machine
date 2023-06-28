using System;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iQuest.VendingMachine.UseCases;
using Moq;
using System.Collections.Generic;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine;

namespace VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        private Mock<IAuthenticationService> authenticationService;
        private Mock<IEntityFrameworkRepository> inMemoryRepository;
        private Mock<IBuyView> buyView;
        private Mock<IPaymentView> paymentView;

        private BuyUseCase buyUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            authenticationService = new Mock<IAuthenticationService>();
            inMemoryRepository = new Mock<IEntityFrameworkRepository>();
            buyView = new Mock<IBuyView>();
            paymentView = new Mock<IPaymentView>();

            buyUseCase = new BuyUseCase(authenticationService.Object, inMemoryRepository.Object, buyView.Object, paymentView.Object);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_TheUserIsAskedForId()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            

            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(true);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("cash");
            // act
            buyUseCase.Execute();   

            // assert
            buyView.Verify(x => x.RequestId(), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_UserIsAskedToConfirmPayment()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(true);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("cash");
            // act
            buyUseCase.Execute();

            // assert
            buyView.Verify(x => x.ConfirmPayment(product.Name), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_UserIsAskedForPaymentMethod()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(true);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("cash");


            // act
            buyUseCase.Execute();

            // assert
            paymentView.Verify(x => x.AskForPaymentMethod(), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_UserPaysWithCash()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(true);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("cash");
            // act
            buyUseCase.Execute();

            // assert
            paymentView.Verify(x => x.PayWithCash(product.Id, product.Price, product.Name), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_UserPaysWithDebitCard()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(true);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("card");
            // act
            buyUseCase.Execute();

            // assert
            paymentView.Verify(x => x.PrintYourCard(), Times.Once);
            paymentView.Verify(x => x.PayWithCard(product.Id), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_NoCashOrCardSelected_ThrowsInvalidTypeException()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(true);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("a");
 
            // act + assert
            Assert.ThrowsException<InvalidTypeException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_PaymentNotConfirmed_ThrowsCancelationException()
        {
            // arrange
            var product = new Product(0, "name", 0f, 1);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            buyView.Setup(x => x.ConfirmPayment(It.IsAny<string>())).Returns(false);

            // act + assert
            Assert.ThrowsException<CancelationException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_TheProductQuantityDecrements()
        {
            // arrange
            var product = new Product(0, "name", 0.0f, 1);
            buyView.Setup(x => x.ConfirmPayment(product.Name)).Returns(true);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("cash");

            // act
            buyUseCase.Execute();

            // assert
            inMemoryRepository.Verify(x => x.DecrementQuantity(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_TheProductIsDispensed()
        {
            // arrange
            var product = new Product(0, "name", 0.0f, 1);
            buyView.Setup(x => x.ConfirmPayment(product.Name)).Returns(true);
            inMemoryRepository.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(product);
            paymentView.Setup(x => x.AskForPaymentMethod()).Returns("cash");

            // act
            buyUseCase.Execute();

            // assert
            buyView.Verify(x => x.DispenseProduct(It.IsAny<string>()), Times.Once);
        }
    }
}
