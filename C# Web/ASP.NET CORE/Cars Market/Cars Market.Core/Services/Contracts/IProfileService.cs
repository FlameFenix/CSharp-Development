using Cars_Market.Infrastructure.Data.Models;

namespace Cars_Market.Core.Services.Contracts
{
    public interface IProfileService
    {
        public Task<Profile> GetProfileByEmail(string email);

        public Task<Profile> GetProfileById(string profileId);

        public Task<string> GetProfileEmail(string profileId);
    }
}
