using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Wordprocessing;

namespace newMVC.Models.Entities
{
    public class Suppliers
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    public ICollection<Product1> Products { get; set; } = new List<Product1>();
    }
}