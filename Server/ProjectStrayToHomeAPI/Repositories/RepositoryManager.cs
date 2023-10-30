using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        IAnimalRepository _animal = null!;

        IAnimalCommentRepository _animalComments = null!;

        IAnimalImageRepository _animalImages = null!;

        ICityRepository _city = null!;

        IUserRepository _users = null!;

        IUserImageRepository _userImages = null!;

        IUserCityRepository _userCities = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: NOT PROPERTIES?
        public IAnimalRepository Animals
        {
            get
            {
                if (_animal == null)
                {
                    _animal = new AnimalRepository(_context);
                }
                return _animal;
            }
        }

        public IAnimalCommentRepository AnimalComments
        {
            get
            {
                if (_animalComments == null)
                {
                    _animalComments = new AnimalCommentRepository(_context);
                }
                return _animalComments;
            }
        }

        public IAnimalImageRepository AnimalImages
        {
            get
            {
                if (_animalImages == null)
                {
                    _animalImages = new AnimalImageRepository(_context);
                }
                return _animalImages;
            }
        }

        public ICityRepository Cities
        {
            get
            {
                if (_city == null)
                {
                    _city = new CityRepository(_context);
                }
                return _city;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if ( _users == null)
                {
                   _users  = new UserRepository(_context);
                }
                return _users;
            }
        }

        public IUserImageRepository UserImages
        {
            get
            {
                if (_userImages == null)
                {
                   _userImages  = new UserImageRepository(_context);
                }
                return _userImages;
            }
        }

        public IUserCityRepository UserCities
        {
            get
            {
                if ( _userCities == null)
                {
                   _userCities  = new UserCityRepository(_context);
                }
                return _userCities;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
