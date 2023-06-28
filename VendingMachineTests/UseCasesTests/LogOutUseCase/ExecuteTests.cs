using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        [TestMethod]
        public void HavingALogoutUseCaseInstance_WhenExecuted_ThenUserIsAuthenticated()
        {
            Mock<IAuthenticationService> authenticationService = new Mock<IAuthenticationService>();
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object);

            logoutUseCase.Execute();

            authenticationService.Verify(x => x.Logout(), Times.Once);
        }
    }
}
