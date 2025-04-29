using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Student1.Data;
using WebApp.Student1.Models;

namespace WebApp.Student1.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var enrollmentList = _context.Enrollment.Include(x=>x.Students).Include(x=>x.Course).ToList();

            return View(enrollmentList);
        }
    }
}
