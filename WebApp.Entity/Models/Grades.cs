using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Student1.Models
{
    
        [Table("Grades")]
        public class Grades
        {
            [Key]
            public int GradeId { get; set; }
            public string? Grade { get; set; }
            public int EnrollmentId { get; set; }
            //1 enrollment
            public Enrollment? Enrollment { get; set; }
        }
    }
