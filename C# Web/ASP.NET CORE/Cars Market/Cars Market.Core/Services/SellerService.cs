using Cars_Market.Core.Services.Contracts;
using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Core.Services
{
    public class SellerService : ISellerService
    {
        private readonly ApplicationDbContext data;
        public SellerService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task AddSeller(Seller seller)
        {
            await data.Sellers.AddAsync(seller);

            await data.SaveChangesAsync();
        }

        public async Task<ICollection<Seller>> ListOfSellers()
        {
            return await data.Sellers.ToListAsync();
        }

        public Task RemoveSeller()
        {
            throw new NotImplementedException();
        }
    }
}
