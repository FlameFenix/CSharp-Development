using Cars_Market.Infrastructure.Data.Seed.Accounts;
using System;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed
{
    public class Dataset
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            await AdminAccount.AccountInitializer(serviceProvider);
            
            await ModeratorAccount.AccountInitializer(serviceProvider);
            
            await UserAccount.AccountInitializer(serviceProvider);
        }

       
    }
}
