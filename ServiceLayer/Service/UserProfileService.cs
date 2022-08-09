using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

namespace ServiceLayer.Service
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserProfileService(IUserProfileRepository userProfileRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userProfileRepository = userProfileRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public void UpdateUserProfile(UpdateUserProfileModel userProfile, string userName)
        {

            var userProfileToUpdate = mapper.Map<UserProfile>(userProfile);

            var user = userManager.FindByNameAsync(userName);
            userProfileToUpdate.ApplicationUser = user.Result;
            this.userProfileRepository.UpdateUserProfile(userProfileToUpdate);
        }
    }
}
