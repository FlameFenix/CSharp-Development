using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services
{
    public class CartService : ICartService
    {
        private readonly SMSDbContext data;
        public CartService(SMSDbContext _data)
        {
            data = _data;
        }
        public void AddProduct(string productId, string userId)
        {
            var product = data.Products.FirstOrDefault(x => x.Id == productId);

            var user = data.Users.Where(x => x.Id == userId).Include(x => x.Cart).FirstOrDefault();

            user.Cart.Products.Add(product);

            data.SaveChanges();
        }

        public ICollection<Product> GetProducts(string userId)
        {
            var products = data.Users.Where(x => x.Id == userId)
                                     .Include(x => x.Cart)
                                     .ThenInclude(x => x.Products)
                                     .Select(x => x.Cart.Products)
                                     .FirstOrDefault();

            return products;
        }

        public void BuyProducts(string userId)
        {
            var products = data.Users.Where(x => x.Id == userId)
                                     .Include(x => x.Cart)
                                     .ThenInclude(x => x.Products)
                                     .FirstOrDefault();

            products.Cart.Products.Clear();

            data.SaveChanges();
        }

    }
}
