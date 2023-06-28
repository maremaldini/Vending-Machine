using System;
using iQuest.VendingMachine.Repositories.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using iQuest.VendingMachine.Exceptions;
using System.Data.Entity;

namespace iQuest.VendingMachine
{
    public class EntityFrameworkRepository : IEntityFrameworkRepository
    {
        private readonly ApplicationDbContext context;

        List<int> IDs = new List<int>();

        public EntityFrameworkRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<int> GetIDs()
        {
            foreach (var product in GetAllProducts())
            {
                IDs.Add(product.Id);
            }

            return IDs;
        }

        public void DecrementQuantity(int id)
        {
            Product product = GetProductById(id);

            if (product.Quantity > 0)
            {
                product.Quantity--;
            }
            else
            {
                throw new OutOfStockException();
            }

            context.SaveChanges();
        }

        private int SetId(List<int> privateIDs)
        {
            if (privateIDs == null || !privateIDs.Any())
            {
                throw new InvalidTypeException();
            }

            int missingID = 1;

            foreach(var id in privateIDs)
            {
                if (id == missingID) missingID++;

            }

            return missingID;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.OrderBy(product => product.Id);
        }

        public Product GetProductById(int id)
        {
            Product product = context.Products.Where(product => product.Id == id).FirstOrDefault();

            if (product != null)
            {
                return product;
            }

            throw new InvalidIdException();
        }

        public void AddProduct(string name, double price, int quantity)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
            };

            if (product == null)
            {
                throw new InvalidTypeException();
            }

            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product product = GetProductById(id);

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void ChangeTheNameOfTheProduct(int id, string name)
        {
            Product product = GetProductById(id);

            product.Name = name;
           // context.Products.Update(product);
            context.SaveChanges();
        }

        public void ChangeThePriceOfTheProduct(int id, double price)
        {
            Product product = GetProductById(id);

            product.Price = price;
            //context.Products.Update(product);
            context.SaveChanges();
        }

        public void ChangeTheQuantityOfTheProduct(int id, int quantity)
        {
            Product product = GetProductById(id);

            product.Quantity += quantity;
           // context.Products.Update(product);
            context.SaveChanges();
        }
    }
}

// method to add a new product to the database with name, q and price parameters
// method to delete with id Parameter
// change the name of the product - name parameter
// chnge the price - price parameter
// change the name - all with the id parameter