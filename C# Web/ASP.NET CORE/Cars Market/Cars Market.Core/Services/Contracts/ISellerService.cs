using Cars_Market.Infrastructure.Data.Models;

namespace Cars_Market.Core.Services.Contracts
{
    public interface ISellerService
    {
        public Task AddSeller(Seller seller);

        public Task RemoveSeller();

        public Task<ICollection<Seller>> ListOfSellers();
    }
}
