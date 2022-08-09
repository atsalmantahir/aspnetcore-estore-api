using DomainLayer.Data;
using DomainLayer.Models;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(StoreDbContext context) : base(context)
        {

        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            context.UserProfiles.Update(userProfile);
            context.SaveChanges();
        }
    }
}
