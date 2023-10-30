using AutoMapper;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Models.DTO.Animals;

namespace ProjectStray2HomeAPI.Models.Profiles
{
    public class AnimalCommentProfile : Profile
    {
        public AnimalCommentProfile() { 
            CreateMap<Animal_Comment, AnimalCommentDTO>();
        }
    }
}
