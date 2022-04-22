using AutoMapper;
using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Profile = AutoMapper.Profile;

namespace Cars_Market.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCarFormModel, CarDetails>();
            CreateMap<EditCarFormModel, Car>();
            // CreateMap<AddCarFormModel, Car>().ForMember(x => x.MainPicture, cfg => cfg.MapFrom(c => c.Image));

        }
    }
}
