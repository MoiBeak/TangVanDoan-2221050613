using System.ComponentModel.DataAnnotations;

namespace newMVC.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        // Navigation Property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
