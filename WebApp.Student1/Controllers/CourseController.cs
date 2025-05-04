using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Index()
        {
            var courseLst = _context.Courses.Include(x=>x.Instructors).ToList();
            return View(courseLst);
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var course = _context.Courses.Include(x => x.Instructors).FirstOrDefault(x => x.CourseId == id);
            return View(course);
        }

        [Authorize(Roles = "admin")]

        [HttpPost]
        public IActionResult Create(Courses course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            return View(course);
        }
        [Authorize(Roles = "admin")]

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
        [Authorize(Roles = "admin")]


        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            _context.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
