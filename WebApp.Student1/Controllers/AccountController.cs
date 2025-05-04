using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Entity.ViewModels;
using WebApp.Student1.Data;
using WebApp.Student1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Student1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [Authorize(Roles = "admin")]

        [HttpGet]
        public IActionResult ListUsers()
        {
            var user = _userManager.Users;
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new()
                { 
                    UserName = model.UserName,
                    Email = model.UserName,
                };
                IdentityResult result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    Students student = new()
                    {
                        StudentName = model.UserName,
                        
                        IdentityUserId = user.Id
                    };
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(user, "student");


                    return RedirectToAction(nameof(Login));
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (await _userManager.IsInRoleAsync(user, "admin"))
                    {
                        return RedirectToAction("AdminIndex", "Account"); 
                    }
                    else if (await _userManager.IsInRoleAsync(user, "student"))
                    {
                        return RedirectToAction("Index", "Student");
                    }

                   
                    return RedirectToAction("AccessDenied");
                }

                ModelState.AddModelError(string.Empty, "Invalid Credentials");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> AccessDenied(string ReturnUrl)
        {
            return View();

        }

        [Authorize(Roles = "admin")]

        [HttpGet]
        public async Task<IActionResult> AddRemoveRoles(string id)
        {
            List<UserRoleViewModel> model = new();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            ViewBag.UserName = user.UserName;
            ViewBag.Id = user.Id;

            foreach (var role in _roleManager.Roles.ToList())
            {
                UserRoleViewModel roleViewModel = new()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name),
                };

                model.Add(roleViewModel);
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddRemoveRoles(List<UserRoleViewModel> model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            ViewBag.UserName = user.UserName;
            ViewBag.Id = user.Id;

            foreach (var role in model)
            {
                if (role.IsSelected && !await _userManager.IsInRoleAsync(user, role.RoleName))
                {
                    var result = await _userManager.AddToRoleAsync(user, role.RoleName);
                }
                else if (!role.IsSelected && await _userManager.IsInRoleAsync(user, role.RoleName))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }

            return RedirectToAction(nameof(ListUsers));
        }

        public async Task<IActionResult> AdminIndex()

        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Id = user.Id;
            return View();
        }

    }
}
