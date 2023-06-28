using System;
using System.Collections.Generic;
using iQuest.VendingMachine.Exceptions;
using iQuest.VendingMachine.PresentationLayer;

namespace iQuest.VendingMachine
{
    internal class VendingMachineApplication : DisplayBase
    {
        private readonly List<IUseCase> useCases;
        private readonly MainView mainView;

        public VendingMachineApplication(List<IUseCase> useCases, MainView mainView)
        {
            this.useCases = useCases ?? throw new ArgumentNullException(nameof(useCases));
            this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
        }

        public void Run()
        {
            mainView.DisplayApplicationHeader();

            bool MaldiniIsJmek = true;

            while (MaldiniIsJmek)
            {
                try
                {
                    List<IUseCase> availableUseCases = GetExecutableUseCases();

                    IUseCase useCase = mainView.ChooseCommand(availableUseCases);
                    useCase.Execute();
                }

                catch (CancelationException)
                {
                    Display("\nCancelation!", ConsoleColor.Red);
                }

                catch (InvalidTypeException)
                {
                    Display("\nInvalid type exception!", ConsoleColor.Red);
                }

                catch (OutOfStockException)
                {
                    Display("Product out of stock!", ConsoleColor.Red);
                }

                catch (InvalidIdException)
                {
                    Display("\nInvalid ID!", ConsoleColor.Red);
                }

                catch (TooBigMoneyException)
                {
                    Display("\nThe vending machine does not accept more than 1000$!", ConsoleColor.Red);
                }


                catch (Exception e) // Trebe sa fie ultimul
                {
                    DisplayLine($"We had an unknown exception: {e.Message}.", ConsoleColor.Red);
                }
               
            }
        }

        private List<IUseCase> GetExecutableUseCases()
        {
            List<IUseCase> executableUseCases = new List<IUseCase>();

            foreach (IUseCase useCase in useCases)
            {
                if(useCase.CanExecute)
                    executableUseCases.Add(useCase);
            }

            return executableUseCases;
        }
    }
}