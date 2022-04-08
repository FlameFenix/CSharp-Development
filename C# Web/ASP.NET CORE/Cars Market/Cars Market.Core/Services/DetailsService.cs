using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services
{
    public class DetailsService : IDetailsService
    {
        private readonly ApplicationDbContext data;
        public DetailsService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task<Car> ReturnDetails(string carId)
        {
                var car = await data.Cars.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id.ToString() == carId);

                car.Details = await data.CarDetails.FirstOrDefaultAsync(x => car.Id == x.CarId);

                car.Comments = await data.Comments.Where(x => x.CarId.ToString() == carId).ToListAsync();

                car.Seller = await data.Sellers.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == car.SellerId);

                car.Pictures = await data.CarPictures.Where(x => x.CarId.ToString() == carId).ToListAsync();

                return car;
        }

        public ICollection<string> GetCarPictures(Car car)
        {
            return car.Pictures.Select(x => Convert.ToBase64String(x.Picture)).ToList();
        }

        public async Task<string> GetUserPictures(string email)
        {
            var sellerPicture = await data.Sellers.Where(x => x.Email == email).Select(x => Convert.ToBase64String(x.Profile.Picture)).FirstOrDefaultAsync();

            return sellerPicture;
        }

        public async Task CountCarVisits(string carId)
        {
            var car = await data.Cars.Include(x => x.Details).FirstOrDefaultAsync(x => x.Id.ToString() == carId);

            car.Details.Visits++;

            await data.SaveChangesAsync();
        }

        public async Task AddCommentToCar(string carId,string email, string commentText)
        {
            var user = await data.Sellers.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Email == email);

            var car = await data.Cars.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id.ToString() == carId);

            var comment = new Comment()
            {
                AuthorName = user.Profile.Name,
                AuthorPicture = user.Profile.Picture,
                Text = commentText,
                CarId = car.Id
            };

            await data.Comments.AddAsync(comment);

            await data.SaveChangesAsync();
        }

    }
}
