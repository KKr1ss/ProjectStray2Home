using AutoMapper;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Models.DTO.User;

namespace ProjectStray2HomeAPI.Models.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(dest => dest.CurrentCity,
                opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.User_Cities,
                opt => opt.MapFrom(src => src.User_Cities.Select(x => x.City).ToList()))
                .ReverseMap();

            CreateMap<ApplicationUser, UserProfileDTO>()
                .ForMember(dest => dest.CurrentCity,
                opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.User_Cities,
                opt => opt.MapFrom(src => src.User_Cities.Select(x => x.City.Name).ToList()))
                .ReverseMap();

            CreateMap<RegisterRequestDTO, ApplicationUser>()
                .ForMember(dest => dest.Sex,
                opt => opt.MapFrom(src => Enum.Parse<Sex>(src.Sex, true)))
                .ReverseMap();

            CreateMap<ApplicationUser, UserPreviewDTO>()
                .ForMember(dest => dest.CurrentCity,
                opt => opt.MapFrom(src => src.City.Name))
                .ReverseMap();
        }
    }
}
