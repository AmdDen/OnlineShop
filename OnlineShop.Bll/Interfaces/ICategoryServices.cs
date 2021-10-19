using OnlineShop.Common.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Bll.Interfaces
{
    public interface ICategoryServices
    {
        //GetAll
        public Task<IEnumerable<CategoryDto>> GetAllCategories();
        //GetById
        public Task<CategoryDto> GetCategoryById(int id);
        //Create
        public Task<CategoryDto> CreateCategory(CreateCategoryDto dto);
        //Update
        public Task<CategoryDto> UpdateCategory(int id, UpdateCategoryDto dto);
        //Delete
        public Task DeleteCateogry(int id);
    }
}
