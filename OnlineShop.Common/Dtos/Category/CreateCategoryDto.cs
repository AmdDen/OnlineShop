using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Common.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
