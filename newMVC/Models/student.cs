using System.ComponentModel.DataAnnotations;

namespace newMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentCode { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}