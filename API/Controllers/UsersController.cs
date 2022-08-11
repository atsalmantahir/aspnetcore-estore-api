using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ObjectResult> Login([FromBody] LoginModel loginModel)
        {
            var response = await this.userService.Login(loginModel);
            return response;
        }

        [HttpPost]
        [Route("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var response = await this.userService.Register(registerModel, DomainLayer.Models.Enums.UserRole.ADMIN);
            return response;
        }

        [HttpPost]
        [Route("register/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel registerModel)
        {
            var response = await this.userService.Register(registerModel, DomainLayer.Models.Enums.UserRole.USER);
            return response;
        }

        [HttpPost]
        [Route("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterModel registerModel)
        {
            var response = await this.userService.Register(registerModel, DomainLayer.Models.Enums.UserRole.CUSTOMER);
            return response;
        }
    }
}
