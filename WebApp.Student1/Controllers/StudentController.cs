using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Student1.Data;
using WebApp.Student1.Models;

namespace WebApp.Student1.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var studentList = _context.Students.Include(x => x.Enrollment).ToList(); 
           
            return View(studentList);
        }
       
        //public IActionResult EnrollToCourse(int StudentId, int courseId)
        //{
        //    var enrollment = new Enrollment()
        //    {
        //        StudentId = StudentId,
        //        CourseId = courseId,
        //        EnrollmentDate = DateTime.Now

        //    };
        //    _context.Enrollment.Add(enrollment);
        //    _context.SaveChanges();
        //    return View(enrollment);

        //}
    }
}
