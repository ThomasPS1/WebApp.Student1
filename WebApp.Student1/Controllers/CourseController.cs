using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Student1.Data;
using WebApp.Student1.Models;

namespace WebApp.Student1.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var courseLst = _context.Courses.Include(x=>x.Instructors).ToList();
            return View(courseLst);
        }
        public IActionResult Details(int id)
        {
            var course = _context.Courses.Include(x => x.Instructors).FirstOrDefault(x => x.CourseId == id);
            return View(course);
        }
        [HttpPost]
        public IActionResult Create(Courses course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Courses course)
        {
            if(ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            _context.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
