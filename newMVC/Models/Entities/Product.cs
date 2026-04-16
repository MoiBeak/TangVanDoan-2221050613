using System.ComponentModel.DataAnnotations;

namespace newMVC.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProductName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
