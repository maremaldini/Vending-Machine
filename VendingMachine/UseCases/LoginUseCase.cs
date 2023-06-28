using System;
using iQuest.VendingMachine.Authentication;
using iQuest.VendingMachine.Authentication.Interfaces;
using iQuest.VendingMachine.PresentationLayer;
using iQuest.VendingMachine.PresentationLayer.Interfaces;

namespace iQuest.VendingMachine.UseCases
{
    public class LoginUseCase : IUseCase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ILogInView loginView;

        public string Name => "login";

        public string Description => "Get access to administration section.";

        public bool CanExecute => !authenticationService.IsUserAuthenticated;

        public LoginUseCase(IAuthenticationService authenticationService, ILogInView loginView)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.loginView = loginView ?? throw new ArgumentNullException(nameof(loginView));
        }

        public void Execute()
        {
            string password = loginView.AskForPassword();
            authenticationService.Login(password);
        }
    }
}