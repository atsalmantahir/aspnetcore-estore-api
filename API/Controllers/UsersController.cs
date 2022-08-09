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
            this.HttpContext.Response.StatusCode = (int)response.StatusCode;
            return new ObjectResult(response);
        }

        [HttpPost]
        [Route("register/admin")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            await this.userService.Register(registerModel);
            return Ok();
        }
    }
}
