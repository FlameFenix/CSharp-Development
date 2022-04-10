using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var seller = await data.Sellers.FirstOrDefaultAsync(x => x.Id.ToString() == sellerId);

            return seller;
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

        public async Task<List<Seller>> GetSellers()
        {
            return await data.Sellers.Include(x => x.Profile).ToListAsync();
        }

        public async Task<ICollection<Seller>> ListOfSellers()
        {
            return await data.Sellers.ToListAsync();
        }
    }
}
