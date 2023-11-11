using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Models.DTO.Animals;
using ProjectStrayToHomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ProjectStrayToHomeAPI.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AnimalService(UserManager<ApplicationUser> userManager, IRepositoryManager repositoryManager, IMapper mapper)
        {
            _userManager = userManager;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<APIResultDTO> UploadAsync(AnimalRequestDTO animalRequest, List<IFormFile> animalImages)
        {

            var animal = _mapper.Map<Animal>(animalRequest);

            var dateNow = DateTime.Now;
            animal.CreateDate = dateNow;
            animal.UpdateDate = dateNow;
            animal.StatusDate = dateNow;

            var userID = (await _userManager.FindByNameAsync(animalRequest.UserName)).Id;
            if (userID == null)
                return new APIResultDTO()
                {
                    Success = false,
                    Message = "Feltöltés sikertelen."
                };
            animal.UserID = userID;

            _repositoryManager.Animals.Create(animal);
            await _repositoryManager.SaveAsync();


            await _repositoryManager.AnimalImages.SetAnimalImagesAsync(animalImages, animal.Id);
            await _repositoryManager.SaveAsync();

            return new APIResultDTO
            {
                Success = true,
                Message = "Sikeres feltöltés"
            };

        }

        public async Task<APIResultDTO> UploadCommentAsync(AnimalCommentRequestDTO commentRequest)
        {
            string userId = (await _userManager.FindByNameAsync(commentRequest.UserName)).Id;
            if (userId == null)
                return new APIResultDTO()
                {
                    Success = false,
                    Message = "Feltöltés sikertelen."
                };

            DateTime date = DateTime.Now;
            Animal_Comment animal_Comment = new()
            {
                UserID = userId,
                AnimalID = commentRequest.AnimalId,
                Comment = commentRequest.Comment,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            _repositoryManager.AnimalComments.Create(animal_Comment);
            await _repositoryManager.SaveAsync();

            return new APIResultDTO
            {
                Success = true,
                Message = "Sikeres feltöltés"
            };
        }

        public async Task<AnimalDetailsDTO?> GetAnimalDetails(int id)
        {
            try
            {
                var animal = await _repositoryManager.Animals.FindByIDAsync(id);
                animal = await _repositoryManager.Animals.GetAnimalConnectedAsync(animal, true);

                var animalDTO = _mapper.Map<AnimalDetailsDTO>(animal);

                return animalDTO;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public async Task<PaginatorResultDTO<AnimalPreviewDTO>> GetAnimals(int pageIndex, int pageSize, string? name, string? status, string? city, string? type, string? sex)
        {
            List<Animal> data = (await _repositoryManager.Animals.FindAllAsync()).ToList();
            data = await _repositoryManager.Animals.GetAnimalsConnectedAsync(data, false);
            data = _repositoryManager.Animals.GetFilteredAnimals(data, name, status, city, type, sex);
            data = _repositoryManager.Animals.GetPaginatedAnimals(data, pageIndex, pageSize);

            var mappedData = new List<AnimalPreviewDTO>();
            foreach (var animal in data)
            {
                mappedData.Add(_mapper.Map<AnimalPreviewDTO>(animal));
            }

            var totalCount = (await _repositoryManager.Animals.FindAllAsync()).Count();
            var result = new PaginatorResultDTO<AnimalPreviewDTO>(mappedData, pageIndex, pageSize, totalCount);

            return result;
        }
    }
}
