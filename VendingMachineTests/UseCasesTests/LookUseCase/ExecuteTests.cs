using System;
using System.Collections.Generic;
using iQuest.VendingMachine;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;
using iQuest.VendingMachine.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VendingMachine.Tests.UseCases.LookUseCaseTests
{
    public class ExecuteTests
    {
        /*
        public void Execute()
        {
            lookView.DisplayProducts(inMemoryRepository.GetAllProducts());
        }
        */

        private Mock<ILookView> lookView;
        private Mock<IEntityFrameworkRepository> inMemoryRepository;
        LookUseCase lookUseCase;

        [TestInitialize]
        public void TestInitialize()
        {
            lookView = new Mock<ILookView>();
            inMemoryRepository = new Mock<IEntityFrameworkRepository>();
            lookUseCase = new LookUseCase(lookView.Object, inMemoryRepository.Object);
        }

        [TestMethod]
        public void HavingLookUseCaseInstance_WhenExecuted_DisplayingTheProducts()
        {
            var products = new List<Product>()
            {
                new Product(0, "name", 0f, 0)
            };

            lookUseCase.Execute(); 

            inMemoryRepository.Setup(x => x.GetAllProducts()).Returns(products);

            lookView.Verify(x => x.DisplayProducts(products), Times.Once);
            
            // ????????????
            
            lookUseCase.Execute();

            lookView.Verify(x => x.DisplayProducts(It.IsAny<List<Product>>()));
            
        }
    }
}