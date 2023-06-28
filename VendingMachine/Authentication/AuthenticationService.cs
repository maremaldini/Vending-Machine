using iQuest.VendingMachine.Authentication.Interfaces;

namespace iQuest.VendingMachine.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsUserAuthenticated { get;  set; }

        public void Login(string password)
        {
            if (password == "aaaa")
                IsUserAuthenticated = true;
            else
                throw new InvalidPasswordException();
        }

        public void Logout()
        {
            IsUserAuthenticated = false;
        }
    }
}