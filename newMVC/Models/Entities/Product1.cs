
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newMVC.Models.Entities
    {
        public class Product1
        {
            [Required]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
            public int SupplierId { get; set; }
    
            
            // Navigation Properties
            [ForeignKey("CategoryId")]
            public Categories? Category { get; set; }

            [ForeignKey("SupplierId")]
            public Suppliers? Supplier { get; set; }
            public ICollection<ImportInvoiceDetail> ImportInvoiceDetails { get; set; } = new List<ImportInvoiceDetail>();
            public ICollection<ExportInvoiceDetail> ExportInvoiceDetails { get; set; } = new List<ExportInvoiceDetail>();
        }
    }