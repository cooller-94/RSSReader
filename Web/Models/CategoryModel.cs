using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CategoryModel
    {
        [Required]
        public string Name { get; set; }
    }
}
