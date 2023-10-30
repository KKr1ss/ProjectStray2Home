namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IAnimalRepository Animals { get; }
        IAnimalCommentRepository AnimalComments { get; }
        IAnimalImageRepository AnimalImages { get; }

        IUserRepository Users { get; }
        IUserImageRepository UserImages { get; }
        IUserCityRepository UserCities { get; }

        ICityRepository Cities { get; }
        
        Task<int> SaveAsync();
    }
}
