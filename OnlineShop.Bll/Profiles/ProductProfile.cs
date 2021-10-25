using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineShop.Common.Dtos.Product;
using OnlineShop.Domain;

namespace OnlineShop.Bll.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(z=> z.CategoryId));
            
            CreateMap<UpdateProductDto, Product>();
        }
    }
}