using DomainLayer.Models;
using DomainLayer.Models.ViewModels;

namespace ServiceLayer.IService
{
    public interface IUserProfileService
    {
        void UpdateUserProfile(UpdateUserProfileModel userProfile, string userName);
    }
}
