using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Data;
// using NuGet.Common;
// using static ClosedXML.Excel.XLPredefinedFormat;
using DateTime = System.DateTime;

namespace newMVC.Models.Entities
{
    public class ImportInvoices
    {
        [Key]
        public int Id { get; set; }
        public DateTime ImportDate { get; set; } = DateTime.Now;
        [Required]
        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        [ForeignKey("SupplierId")]
        public Suppliers? Supplier { get; set; }
        public ICollection<ImportInvoiceDetail> ImportInvoiceDetails { get; set; }

        public ImportInvoices()
        {
            ImportInvoiceDetails = new List<ImportInvoiceDetail>();
        }
    }
}