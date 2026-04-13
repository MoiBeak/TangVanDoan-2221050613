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

        // Bảng Sinh viên
        public DbSet<Student> Students { get; set; }

        // Bảng Khoa
        public DbSet<Faculty> Faculties { get; set; }

        // (Tùy chọn) Cấu hình thêm quan hệ bằng Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa dây chuyền (cascade delete)
        }
    }
}
