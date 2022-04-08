using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext data;
        public ProfileService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task<Profile> GetProfileByEmail(string email)
        {
            return await data.Sellers
                .Include(x => x.Profile)
                .Where(x => x.Email == email)
                .Select(x => x.Profile)
                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetProfileById(string profileId)
        {
            return await data.Profiles.FirstOrDefaultAsync(x => x.Id.ToString() == profileId);
        }

        public async Task<string> GetProfileEmail(string profileId)
        {
            return await data.Profiles.Include(x => x.Seller)
                                      .Where(x => x.Id.ToString() == profileId)
                                      .Select(x => x.Seller.Email)
                                      .FirstOrDefaultAsync(); 
        }
    }
}
