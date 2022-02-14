using SMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    public interface ICartService
    {
        public void AddProduct(string productId, string userId);

        public ICollection<Product> GetProducts(string userId);

        public void BuyProducts(string userId);
    }
}
