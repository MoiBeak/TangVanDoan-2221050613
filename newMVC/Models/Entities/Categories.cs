
using System.ComponentModel.DataAnnotations;

namespace newMVC.Models.Entities
{
    public class Categories
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product1> Products { get; set; } = new List<Product1>();
    }
}