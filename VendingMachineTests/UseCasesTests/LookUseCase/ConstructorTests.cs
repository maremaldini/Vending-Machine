using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachine.Tests.UseCases.LookUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        private Mock<IEntityFrameworkRepository> inMemoryRepository;
        private Mock<ILookView> lookView;

        [TestInitialize]
        public void TestSetup()
        {
            inMemoryRepository = new Mock<IEntityFrameworkRepository>();
            lookView = new Mock<ILookView>();
        }

        [TestMethod]
        public void HavingANullInMemoryRepository_WhenInitializingTheLookUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LookUseCase(lookView.Object, null);
            });
        }

        public void HavingANullLookView_WhenInitializingTheLookUseCase_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LookUseCase(null, inMemoryRepository.Object);
            });
        }

        [TestMethod]
        public void HappyFlowBuyUseCase_WhenInitializingTheUseCase_NoExceptionIsThrown()
        {
            Assert.IsNotNull(new LookUseCase(lookView.Object, inMemoryRepository.Object));
        }
    }
}

