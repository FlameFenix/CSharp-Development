using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
