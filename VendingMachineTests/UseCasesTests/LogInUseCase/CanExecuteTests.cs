using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    [TestClass]
    public class CanExecuteTests
    {
        private Mock<IAuthenticationService> authenticationService;
        private Mock<ILogInView> loginView;

        [TestInitialize]
        public void TestSetup()
        {
            loginView = new Mock<ILogInView>();

            authenticationService = new Mock<IAuthenticationService>();
        }

        [TestMethod]
        public void HavingNoAdminLoggedIn_CanExecuteIsTrue()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(false);
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            Assert.IsTrue(loginUseCase.CanExecute);
        }

        [TestMethod]
        public void HavingAdminLoggedIn_CanExecuteIsFalse()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(true);
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            Assert.IsFalse(loginUseCase.CanExecute);
        }
    }
}
