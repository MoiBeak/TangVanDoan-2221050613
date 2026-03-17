using System.ComponentModel.DataAnnotations;

namespace newMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(10, ErrorMessage = "Mã sinh viên tối đa 10 ký tự")]
        public string StudentCode { get; set; }

        [Required(ErrorMessage = "Tên sinh viên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên sinh viên tối đa 50 ký tự")]
        public string FullName { get; set; }

        [Range(18, 30, ErrorMessage = "Tuổi phải từ 18 đến 30")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
    }
}