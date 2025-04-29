using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Student1.Models
{

    [Table("Courses")]
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public int InstructorId { get; set; }
        //one instructor
        public Instructors? Instructors { get; set; }
        //list of enrollment
        public List<Enrollment>? Enrollment { get; set; }

    }
}


