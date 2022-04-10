using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(ByteConverter))
                    .AddTransient(typeof(ProfileService))
                    .AddTransient(typeof(MessageService))
                    .AddTransient(typeof(DetailsService))
                    .AddTransient(typeof(ContactsService))
                    .AddTransient(typeof(CarsService))
                    .AddTransient(typeof(SellerService));


            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
