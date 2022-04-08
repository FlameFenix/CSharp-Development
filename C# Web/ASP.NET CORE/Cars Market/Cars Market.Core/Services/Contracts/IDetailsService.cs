using Cars_Market.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services.Contracts
{
	internal interface IDetailsService
	{
		public Task<Car> ReturnDetails(string carId);

        public ICollection<string> GetCarPictures(Car car);

		public Task<string> GetUserPictures(string email);

		public Task CountCarVisits(string carId);

		public Task AddCommentToCar(string carId, string email, string commentText);

	}
}
