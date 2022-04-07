using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cars_Market.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Test.Mocks
{
    public static class CarsMarketDbContextMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                return new ApplicationDbContext(dbContextOptions);
            }
        }
    }
}
