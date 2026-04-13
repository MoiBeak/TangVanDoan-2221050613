using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newMVC.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(10, ErrorMessage = "Mã sinh viên tối đa 10 ký tự")]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sinh viên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên sinh viên tối đa 50 ký tự")]
        public string FullName { get; set; } = string.Empty;

        [Range(18, 30, ErrorMessage = "Tuổi phải từ 18 đến 30")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        // Khóa ngoại
        public int FacultyId { get; set; }

        // Navigation Property
        public Faculty? Faculty { get; set; }
    }
}
