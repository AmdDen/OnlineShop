using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Bll.Interfaces;
using OnlineShop.Common.Dtos.Category;
using OnlineShop.Dal;
using OnlineShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Bll.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryServices(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _repository.Add(category);
            await _repository.Save();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
        public async Task DeleteCateogry(int id)
        {
            var category = await _repository.Find(id);
            _repository.Delete(category);
            await _repository.Save();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categoryList = await _repository.GetAll().ToListAsync();
            var categoryListDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryList);

            return categoryListDto;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await _repository.Find(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public async Task<CategoryDto> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            var category = await _repository.Find(id);

            if (category == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                category.Name = dto.Name;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                category.Description = dto.Description;
            }

            await _repository.Save();
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;

        }
    }
}
