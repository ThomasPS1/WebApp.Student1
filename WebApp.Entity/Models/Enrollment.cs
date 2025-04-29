using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Student1.Models
{

    [Table("Enrollment")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        //one student
        public Students? Students { get; set; }
        //one grade
        public Grades? Grades { get; set; }
        //one course
        public Courses? Courses { get; set; }

    }
}



