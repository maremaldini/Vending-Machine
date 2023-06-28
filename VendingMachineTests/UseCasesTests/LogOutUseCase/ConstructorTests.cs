using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void HavingANullAuthenticationService_WhenInitializingTheUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LogoutUseCase(null);
            });
        }
    }
}
