using System;
using System.Collections;
using System.Collections.Generic;
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
    public class CanExecuteTests
    {
        private Mock<ILookView> lookView;
        private Mock<IEntityFrameworkRepository> inMemoryRepository;


        [TestInitialize]
        public void TestSetup()
        {
            lookView = new Mock<ILookView>();
            inMemoryRepository = new Mock<IEntityFrameworkRepository>();
        }

        [TestMethod]
        public void CheckIfCanExecuteIsTrue_IsTrue()
        {
            // arrange
            LookUseCase lookUseCase = new LookUseCase(lookView.Object, inMemoryRepository.Object);

            // act + assert
            Assert.IsTrue(lookUseCase.CanExecute);
        }
    }
}