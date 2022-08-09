using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IUserProfileRepository : IGenericRepository<UserProfile>
    {
        void UpdateUserProfile(UserProfile userProfile);
    }
}
