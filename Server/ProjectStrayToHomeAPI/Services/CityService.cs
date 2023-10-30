using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Services
{
    public class CityService : ICityService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CityService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetCitiesAsync()
        {
            var cities = await _repositoryManager.Cities.FindAllAsync();
            var citiesDTO = cities.Select(c => _mapper.Map<CityDTO>(c));
            return citiesDTO;
        }
    }
}
