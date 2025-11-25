using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPPDEMO.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        [Display(Name = "Teacher ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Teacher Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Display(Name = "Teacher Name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [StringLength(80)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [StringLength(30)]
        public string Course { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}

