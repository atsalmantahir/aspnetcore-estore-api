using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase
    {
        private IUserProfileService userProfileService { get; set; }
        public UserProfilesController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpPut]       
        public async Task<IActionResult> UpdateProduct(UpdateUserProfileModel userProfileModel)
        {
            string loggedInUserName = HttpContext.User.Identity.Name;

            this.userProfileService.UpdateUserProfile(userProfileModel, loggedInUserName);
            return Ok();
        }
    }
}
