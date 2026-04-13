using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newMVC.Models.Entities
{
    public class Faculty
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khoa tối đa 100 ký tự")]
        public string FacultyName { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
