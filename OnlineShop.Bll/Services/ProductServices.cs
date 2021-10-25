using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Bll.Interfaces;
using OnlineShop.Common.Dtos.Product;
using OnlineShop.Common.Exceptions;
using OnlineShop.Dal;
using OnlineShop.Domain;

namespace OnlineShop.Bll.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProductServices(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProduct(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            _repository.Add(product);
            await _repository.Save();

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task DeleteProuct(int id)
        {
            var product = await _repository.Find<Product>(id);

            isProductNullException(product, id);

            _repository.Delete(product);
            await _repository.Save();

        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _repository.GetAll<Product>().ToListAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productsDto;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _repository.GetByIdWithInclude<Product>(id, product => product.Category);

            isProductNullException(product, id);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<ProductDto> UpdateProduct(int id, UpdateProductDto dto)
        {
            var category = await _repository.Find<Category>(dto.CategoryId);
            isCategoryNullException(category, id);

            var product = await _repository.Find<Product>(id);
            isProductNullException(product, id);

            _mapper.Map(dto, product);
            await _repository.Save();

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        private void isProductNullException(Product product, int id)
        {
            if (product == null)
            {
                throw new ValidationException($"{nameof(product)} with ID {id} not found !");
            }
        }

        private void isCategoryNullException(Category category, int id)
        {
            if (category == null)
            {
                throw new EntityNullReferenceException($"{nameof(category)} with ID {id} not found !");
            }
        }
    }
}