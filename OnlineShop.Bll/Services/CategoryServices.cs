using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Bll.Interfaces;
using OnlineShop.Common.Dtos.Category;
using OnlineShop.Common.Exceptions;
using OnlineShop.Dal;
using OnlineShop.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Bll.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CategoryServices(IRepository repository, IMapper mapper)
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
            var category = await _repository.Find<Category>(id);
            isCategoryNullException(category, id);

            _repository.Delete(category);
            await _repository.Save();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categoryList = await _repository.GetAll<Category>().ToListAsync();
            var categoryListDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryList);

            return categoryListDto;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await _repository.Find<Category>(id);

            isCategoryNullException(category, id);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public async Task<CategoryDto> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            var category = await _repository.Find<Category>(id);

            isCategoryNullException(category, id);

            _mapper.Map(dto, category);
            await _repository.Save();

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        private void isCategoryNullException(Category category, int id)
        {
            if (category == null)
            {
                throw new EntityNullReferenceException($"{nameof(Category)} with ID {id} not found!");
            }
        } 
    }
}
