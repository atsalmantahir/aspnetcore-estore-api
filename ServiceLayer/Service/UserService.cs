using DomainLayer.Models;
using DomainLayer.Models.Constants;
using DomainLayer.Models.Response;
using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Handlers;
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
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                    "USER_NOT_FOUND");
            }

            if (!await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                    "INCORRECT_CREDENTIALS");
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

        public async Task<ObjectResult> Register(RegisterModel registerModel)
        {
            
            var userNameExists = await userManager.FindByNameAsync(registerModel.Username);
            var emailExists = await userManager.FindByEmailAsync(registerModel.Email);
            if (userNameExists != null || emailExists != null) 
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict,
                "USER_ALREADY_EXISTS");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.Username
            };
            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded) 
            {
                switch (registerModel.userRole)
                {
                    case DomainLayer.Models.Enums.UserRole.ADMIN:
                        // Add Admin
                        await roleManager.CreateAsync(new IdentityRole(UserRole.ADMIN));
                        break;
                    case DomainLayer.Models.Enums.UserRole.USER:
                        // Add User
                        await roleManager.CreateAsync(new IdentityRole(UserRole.USER));
                        break;
                    case DomainLayer.Models.Enums.UserRole.CUSTOMER:
                        // Add Customer
                        await roleManager.CreateAsync(new IdentityRole(UserRole.CUSTOMER));
                        break;
                    default:
                        throw new HttpStatusCodeException(HttpStatusCode.UnprocessableEntity,
                    "NO_USER_ROLE_FOUND");
                }

                return new CreatedResult(String.Empty, "User Created");
            }

            throw new HttpStatusCodeException(HttpStatusCode.Conflict,
                result.Errors.FirstOrDefault().Description);
            
        }
    }
}
