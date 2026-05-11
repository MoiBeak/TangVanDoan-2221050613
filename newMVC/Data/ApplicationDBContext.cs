using Microsoft.EntityFrameworkCore;
using newMVC.Models.Entities;

namespace newMVC.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // =========================
        // Existing Tables
        // =========================

        // Bảng Sinh viên
        public DbSet<Student> Students { get; set; }

        // Bảng Khoa
        public DbSet<Faculty> Faculties { get; set; }

        // =========================
        // Tables for Practice 9
        // =========================

        // Bảng Khách hàng
        public DbSet<Customer> Customers { get; set; }

        // Bảng Sản phẩm
        public DbSet<Product> Products { get; set; }

        // Bảng Đơn hàng
        public DbSet<Order> Orders { get; set; }

        // Bảng Chi tiết đơn hàng
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // =========================
        // TABLES FOR WAREHOUSE MANAGEMENT
        // =========================

        // Nhà cung cấp
        public DbSet<Suppliers> Suppliers { get; set; }

        // Loại thiết bị
        public DbSet<Categories> Categories { get; set; }

        // // Thiết bị
        // public DbSet<Product1> Devices { get; set; }

        // Phiếu nhập
        public DbSet<ImportInvoices> ImportInvoices { get; set; }

        // Chi tiết phiếu nhập
        public DbSet<ImportInvoiceDetail> ImportInvoiceDetails { get; set; }

        // Phiếu xuất
        public DbSet<ExportInvoices> ExportInvoices { get; set; }

        // Chi tiết phiếu xuất
        public DbSet<ExportInvoiceDetail> ExportInvoiceDetails { get; set; }

        // =========================
        // Fluent API Configuration
        // =========================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================================
            // Existing Relationships
            // =====================================

            // Quan hệ: Faculty - Student (1 - N)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ: Customer - Order (1 - N)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ: Order - OrderDetail (1 - N)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ: Product - OrderDetail (1 - N)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================
            // Warehouse Relationships
            // =====================================

            // Product - Category
            modelBuilder.Entity<Product1>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product - Supplier
            modelBuilder.Entity<Product1>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // ImportReceipt - ImportDetail
            modelBuilder.Entity<ImportInvoiceDetail>()
                .HasOne(id => id.ImportInvoice)
                .WithMany(ir => ir.ImportInvoiceDetails)
                .HasForeignKey(id => id.ImportInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - ImportDetail
            modelBuilder.Entity<ImportInvoiceDetail>()
                .HasOne(id => id.Product)
                .WithMany(p => p.ImportInvoiceDetails)
                .HasForeignKey(id => id.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ExportReceipt - ExportDetail
            modelBuilder.Entity<ExportInvoiceDetail>()
                .HasOne(ed => ed.ExportInvoice)
                .WithMany(er => er.ExportInvoiceDetails)
                .HasForeignKey(ed => ed.ExportInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - ExportDetail
            modelBuilder.Entity<ExportInvoiceDetail>()
                .HasOne(ed => ed.Product)
                .WithMany(p => p.ExportInvoiceDetails)
                .HasForeignKey(ed => ed.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================
            // Decimal Configurations
            // =====================================

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product1>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ImportInvoiceDetail>()
                .Property(id => id.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ImportInvoiceDetail>()
                .Property(id => id.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExportInvoiceDetail>()
                .Property(ed => ed.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExportInvoiceDetail>()
                .Property(ed => ed.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ImportInvoices>()
                .Property(ir => ir.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExportInvoices>()
                .Property(er => er.TotalAmount)
                .HasColumnType("decimal(18,2)");
        }
        public DbSet<newMVC.Models.Entities.Product1> Product1 { get; set; } = default!;
    }
}