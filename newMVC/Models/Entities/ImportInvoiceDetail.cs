using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newMVC.Models.Entities
{
    public class ImportInvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ImportInvoiceId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
         public decimal TotalPrice { get; set; }
        // Navigation Properties
        [ForeignKey("ImportInvoiceId")]
        public ImportInvoices? ImportInvoice { get; set; }

        [ForeignKey("ProductId")]
        public Product1? Product { get; set; }
    }
}
