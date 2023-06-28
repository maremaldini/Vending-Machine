using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        private Mock<IAuthenticationService> authenticationService;
        private Mock<ILogInView> loginView;

        [TestInitialize]
        public void TestSetup()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILogInView>();
        }

        [TestMethod]
        public void HavingANullAuthenticationService_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LoginUseCase(null, loginView.Object);
            });
        }

        [TestMethod]
        public void HavingANullLoginView_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LoginUseCase(authenticationService.Object, null);
            });
        }

        [TestMethod]
        public void HappyFlowLogInUseCase_WhenInitializingTheUseCase_NoExceptionIsThrown()
        {
            Assert.IsNotNull(new LoginUseCase(authenticationService.Object, loginView.Object));
        }
    }
}
