using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Models.DTO.Animals;

namespace ProjectStray2HomeAPI.Models.Profiles
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<AnimalRequestDTO, Animal>()
                .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => Enum.Parse<Animal_Type>(src.Type, true)))
                .ForMember(dest => dest.Sex,
                opt => opt.MapFrom(src => Enum.Parse<Sex>(src.Sex, true)))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => Enum.Parse<Animal_Status>(src.Status, true)))
                .ReverseMap();

            CreateMap<Animal, AnimalDetailsDTO>()
                .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.UserPreview,
                opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.AnimalImagesId,
                opt => opt.MapFrom(src => src.Images.Select(x => x.Id).ToArray<int>()))
                .ReverseMap();

            CreateMap<Animal, AnimalPreviewDTO>()
                .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.UserPreview,
                opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
