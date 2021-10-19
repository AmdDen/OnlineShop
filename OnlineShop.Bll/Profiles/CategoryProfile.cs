using AutoMapper;
using OnlineShop.Common.Dtos.Category;
using OnlineShop.Domain;

namespace OnlineShop.Bll.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
