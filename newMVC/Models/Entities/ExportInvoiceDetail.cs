using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newMVC.Models.Entities
{
    public class ExportInvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExportInvoiceId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        // Navigation
        [ForeignKey("ExportInvoiceId")]
        public ExportInvoices? ExportInvoice { get; set; }

        [ForeignKey("ProductId")]
        public Product1? Product { get; set; }

        
        public decimal TotalPrice { get; set; }
    }
}