using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.IService
{
    public interface IUserService
    {
        Task<ObjectResult> Login(LoginModel loginModel);
        Task Register(RegisterModel registerModel);
        Task RegisterAdmin(RegisterModel registerModel);
    }
}
