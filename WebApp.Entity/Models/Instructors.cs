using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Student1.Models
{
    
        [Table("Instructor")]
        public class Instructors
        {
            [Key]
            public int InstructorId { get; set; }
            public string? InstructorName { get; set; }
            //list of course
            public List<Courses>? Courses { get; set; }
        }
    }
