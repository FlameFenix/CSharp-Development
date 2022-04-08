using System;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed
{
    public class Dataset
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            await AccountAndRolesSeed.AccountInitializer(serviceProvider);
        }

       
    }
}
