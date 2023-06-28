
using System.Collections.Generic;

namespace iQuest.VendingMachine.Repositories.Interfaces
{
    public interface IEntityFrameworkRepository
    {
        void AddProduct(string name, double price, int quantity);
        void ChangeTheNameOfTheProduct(int id, string name);
        void ChangeThePriceOfTheProduct(int id, double price);
        void ChangeTheQuantityOfTheProduct(int id, int quantity);
        void DecrementQuantity(int id);
        void DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
        List<int> GetIDs();
        Product GetProductById(int id);
    }
}