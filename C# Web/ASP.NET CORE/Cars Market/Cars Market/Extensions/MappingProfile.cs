using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Profile = AutoMapper.Profile;

namespace Cars_Market.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCarFormModel, Car>();
            CreateMap<AddCarFormModel, CarDetails>();
            CreateMap<EditCarFormModel, Car>();
            CreateMap<EditCarFormModel, CarDetails>();
            CreateMap<AddSellerFormModel, Seller>();
            CreateMap<AddSellerFormModel, Infrastructure.Data.Models.Profile>();
        }
    }
}
