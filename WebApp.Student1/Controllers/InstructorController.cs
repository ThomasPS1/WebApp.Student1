using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Student1.Data;
using WebApp.Student1.Models;

namespace WebApp.Student1.Controllers
{
    [Authorize(Roles = "admin")]
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var instructor = _context.Instructors.ToList();
            return View(instructor);
        }
        public IActionResult Details(int id)
        {
            var instructor = _context.Instructors.Find(id);
            return View(instructor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Instructors instructor)
        {
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = _context.Instructors.Find(id);
            return View(instructor);
        }
        [HttpPost]
        public IActionResult Edit(Instructors instructor)
        {
            _context.Instructors.Update(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructors.Find(id);
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
