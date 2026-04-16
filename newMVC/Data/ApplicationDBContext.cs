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
        // Fluent API Configuration
        // =========================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quan hệ: Faculty - Student (1 - N)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa dây chuyền

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

            // (Tùy chọn) Cấu hình kiểu dữ liệu cho Price
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
