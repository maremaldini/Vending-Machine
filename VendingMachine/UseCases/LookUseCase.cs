using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;
using iQuest.VendingMachine.Repositories.Interfaces;

namespace iQuest.VendingMachine.UseCases
{
    public class LookUseCase :IUseCase
    {
        private readonly ILookView lookView;
        private readonly IEntityFrameworkRepository inMemoryRepository;

        public string Name => "look";

        public string Description => "See all the available products.";

        public bool CanExecute => true;

        public LookUseCase(ILookView lookView, IEntityFrameworkRepository inMemoryRepository)
        {
            this.lookView = lookView ?? throw new ArgumentNullException(nameof(lookView));
            this.inMemoryRepository = inMemoryRepository ?? throw new ArgumentNullException(nameof(inMemoryRepository));
        }

        public void Execute()
        {
            lookView.DisplayProducts(inMemoryRepository.GetAllProducts());
        }
    }
}

