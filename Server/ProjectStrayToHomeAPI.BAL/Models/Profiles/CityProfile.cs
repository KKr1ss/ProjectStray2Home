using AutoMapper;
using ProjectStray2HomeAPI.Models.DTO;
using ProjectStray2HomeAPI.Models.EF;

namespace ProjectStray2HomeAPI.Models.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTO>();
        }
    }
}
