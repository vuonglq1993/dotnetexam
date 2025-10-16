using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ComicContext _context;

        public AccountController(ComicContext context)
        {
            _context = context;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(Customer model)
        {
            if (ModelState.IsValid)
            {
                model.RegistrationDate = DateTime.Now;
                _context.Customers.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string phoneNumber, string password)
        {
            var user = await _context.Customers
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber && x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("CustomerID", user.CustomerID);
                HttpContext.Session.SetString("FullName", user.FullName);
                return RedirectToAction("Index", "ComicBooks");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}