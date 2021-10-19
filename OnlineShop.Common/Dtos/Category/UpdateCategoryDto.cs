using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Common.Dtos.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
