using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.ViewModels;

namespace ServiceLayer.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddProductModel, Product>().ReverseMap();
            CreateMap<ProductsViewModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();

            CreateMap<UpdateUserProfileModel, UserProfile>().ReverseMap();
        }
    }
}
