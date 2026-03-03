using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newMVC.Models
    {
[Table("Student")]
    public class Student
    
{
   [Key]
    public required string StudentCode { get; set; }
    public string FullName { get; set; }
    }
    }