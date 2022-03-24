using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
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

        public async Task<Seller> GetSellerById(string sellerId)
        {
            return await data.Sellers.FirstOrDefaultAsync(x => x.Id.ToString() == sellerId);
        }

        public async Task<Seller> GetSellerByEmail(string sellerEmail)
        {
            return await data.Sellers.FirstOrDefaultAsync(x => x.Email == sellerEmail);
        }

        public async Task<Seller> GetSellerWithMessages(string sellerEmail)
        {
            return await data.Sellers.Include(x => x.Messages).FirstOrDefaultAsync(x => x.Email == sellerEmail);
        }

        public async Task<Seller> GetSellerWithProfile(string sellerEmail)
        {
            return await data.Sellers.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Email == sellerEmail);
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
