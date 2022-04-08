using Cars_Market.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services.Contracts
{
    public interface ISellerService
    {
        public Task AddSeller(Seller seller);

        public Task<Seller> GetSellerById(string sellerId);

        public Task<Seller> GetSellerByEmail(string sellerEmail);

        public Task<Seller> GetSellerWithMessages(string sellerEmail);

        public Task<Seller> GetSellerWithProfile(string sellerEmail);

        public Task RemoveSeller();

        public Task<ICollection<Seller>> ListOfSellers();
    }
}
