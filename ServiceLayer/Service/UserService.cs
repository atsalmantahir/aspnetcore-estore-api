using DomainLayer.Models;
using DomainLayer.Models.Constants;
using DomainLayer.Models.Response;
using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<ObjectResult> Login(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.Username);

            if (user == null)
            {
                return new NotFoundObjectResult("USER_NOT_FOUND")
                {
                    StatusCode = (int?)HttpStatusCode.NotFound,
                };
            }

            if (!await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return new NotFoundObjectResult("INCORRECT_CREDENTIALS")
                {
                    StatusCode = (int?)HttpStatusCode.NotFound,
                };
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenResponse = (new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });

            return new OkObjectResult(tokenResponse);
        }

        public async Task Register(RegisterModel registerModel)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(registerModel.Username);
                if (userExists != null) { }

                ApplicationUser user = new ApplicationUser()
                {
                    Email = registerModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerModel.Username
                };
                var result = await userManager.CreateAsync(user, registerModel.Password);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task RegisterAdmin(RegisterModel registerModel)
        {
            var userExists = await userManager.FindByNameAsync(registerModel.Username);
            if (userExists != null) { }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.Username
            };
            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            { }
            if (!await roleManager.RoleExistsAsync(UserRoles.ADMIN))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));
            if (!await roleManager.RoleExistsAsync(UserRoles.USER))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.USER));

            if (await roleManager.RoleExistsAsync(UserRoles.ADMIN))
            {
                await userManager.AddToRoleAsync(user, UserRoles.ADMIN);
            }
        }
    }
}
