using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newMVC.Models.Entities
{
    public class ExportInvoices
    {
        [Key]
        public int Id { get; set; }
        public DateTime ExportDate { get; set; } = DateTime.Now;
        public string ReceiverName { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        [ForeignKey("SupplierId")]
        public Suppliers? Supplier { get; set; }
        public ICollection<ExportInvoiceDetail> ExportInvoiceDetails { get; set; }

        public ExportInvoices()
        {
            ExportInvoiceDetails = new List<ExportInvoiceDetail>();
        }
    }
}