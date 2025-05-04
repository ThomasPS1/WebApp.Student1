using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Student1.Data;
using WebApp.Student1.Models;

namespace WebApp.Student1.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EnrollmentController(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var enrollmentList = _context.Enrollment.Include(x=>x.Students).Include(x=>x.Course).ToList();

            return View(enrollmentList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null)
            //{
            //    return Unauthorized();
            //}

            var student = await _context.Students.FirstOrDefaultAsync(s => s.IdentityUserId == currentUser.Id);
            //if (student == null)
            //{
            //    return NotFound("Student not found for the logged-in user.");
            //}

            enrollment.StudentId = student.StudentId;

            if (ModelState.IsValid)
            {
                _context.Enrollment.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Student");
            }

            //ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }

    }
}
