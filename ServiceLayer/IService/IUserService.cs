using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.IService
{
    public interface IUserService
    {
        Task<ObjectResult> Login(LoginModel loginModel);
        Task<ObjectResult> Register(RegisterModel registerModel, DomainLayer.Models.Enums.UserRole userRole);
    }
}
