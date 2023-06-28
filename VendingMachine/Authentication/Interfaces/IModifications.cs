namespace iQuest.VendingMachine.Authentication.Interfaces
{
    public interface IModifications
    {
        string GetNewName();
        double GetNewPrice();
        int GetNewQuantity();
    }
}