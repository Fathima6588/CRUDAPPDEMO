using System.ComponentModel.DataAnnotations;

namespace CRUDAPPDEMO.Models
{
    public class Student
    {
        [Key]
        [Display (Name = "Student ID")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Student Name")]
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
    }
}
