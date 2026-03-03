using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using newMVC.Models;

namespace newMVC.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // Ví dụ tạo bảng Product
     
        public DbSet<Student> Students { get; set; }
    }
}