using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachineTests.UseCasesTests.LoginUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        private Mock<IAuthenticationService> authenticationService;
        private Mock<ILogInView> loginView;
        private LoginUseCase loginUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILogInView>();
            loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);
        }

        [TestMethod]
        public void HavingALoginUseCaseInstance_WhenExecuted_ThenUserIsAskedToIntroducePassword()
        {
            //arrange
            //act
            loginUseCase.Execute();

            //assert
            loginView.Verify(x => x.AskForPassword(), Times.Once);
        }

        [TestMethod]
        public void HavingALoginUseCaseInstance_WhenExecuted_ThenUserIsAuthenticated()
        {
            //arrange
            loginView
                 .Setup(x => x.AskForPassword())
                 .Returns("pass123");

            //act
            loginUseCase.Execute();

            //assert
            authenticationService.Verify(x => x.Login("pass123"), Times.Once);
        }
    }
}