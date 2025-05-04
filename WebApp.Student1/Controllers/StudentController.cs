using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Student1.Data;
using WebApp.Student1.Models;

namespace WebApp.Student1.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); 
            var student = _context.Students
                .Include(s => s.Enrollment)
                .ThenInclude(c=>c.Course)
                .FirstOrDefault(s => s.IdentityUserId == user.Id);

            if (student == null)
            {
                return NotFound("Student record not found for this user.");
            }
           
            return View(student);
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
