using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPPDEMO.Models
{
    public class Student
    {
        [Key]
        [Required]

        [Display(Name = "Student ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [StringLength(80)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [StringLength(30)]
        public string Course { get; set; }

        // Make FK nullable so a Student can be created without assigning a Teacher
        [ForeignKey(nameof(Teacher))]
        public int? TeacherId { get; set; }

        // Navigation property
        public Teacher? Teacher { get; set; }
    }
}
