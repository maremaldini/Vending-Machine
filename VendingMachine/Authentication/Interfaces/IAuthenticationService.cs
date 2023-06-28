using System;
namespace iQuest.VendingMachine.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated { get;  set; }

        void Login(string password);

        void Logout();
    }
}