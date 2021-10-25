using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Common.Dtos.Product;

namespace OnlineShop.Bll.Interfaces
{
    public interface IProductServices
    {
        public Task<IEnumerable<ProductDto>> GetAllProducts();
        //GetById
        public Task<ProductDto> GetProductById(int id);
        //Create
        public Task<ProductDto> CreateProduct(CreateProductDto dto);
        //Update
        public Task<ProductDto> UpdateProduct(int id, UpdateProductDto dto);
        //Delete
        public Task DeleteProuct(int id);
    }
}