using Microsoft.AspNetCore.Mvc;
using WebApp.Student1.Data;

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
            var studentList = _context.Students.ToList();
            return View(studentList);
        }
    }
}
