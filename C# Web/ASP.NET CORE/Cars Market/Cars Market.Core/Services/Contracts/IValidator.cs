using Cars_Market.Infrastructure.Data.Models;

namespace Cars_Market.Core.Services.Contracts
{
    public interface IValidator
    {
        public ICollection<string> AddSellerValidation(Seller seller);
    }
}
