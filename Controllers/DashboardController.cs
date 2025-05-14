using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AgriEnergyConnect.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        // --- FARMER DASHBOARD ---
        public IActionResult FarmerDashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || HttpContext.Session.GetString("UserRole") != "Farmer")
                return RedirectToAction("Login", "Account");

            var products = _context.Products
                .Where(p => p.UserId == userId)
                .ToList();

            return View(products);
        }

        [HttpPost]
        public IActionResult AddProduct(string name, string category, DateTime productionDate)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var product = new Product
            {
                Name = name,
                Category = category,
                ProductionDate = productionDate,
                UserId = userId.Value
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("FarmerDashboard");
        }

        // --- EMPLOYEE DASHBOARD ---
        public IActionResult EmployeeDashboard(string category, DateTime? startDate, DateTime? endDate)
        {
            if (HttpContext.Session.GetString("UserRole") != "Employee")
                return RedirectToAction("Login", "Account");

            ViewBag.Farmers = _context.Users.Where(u => u.Role == "Farmer").ToList();

            var products = _context.Products.Include(p => p.User).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);

            if (startDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate.Value);

            if (endDate.HasValue)
                products = products.Where(p => p.ProductionDate <= endDate.Value);

            return View(products.ToList());
        }

        [HttpPost]
        public IActionResult AddFarmer(string email, string password)
        {
            if (HttpContext.Session.GetString("UserRole") != "Employee")
                return RedirectToAction("Login", "Account");

            if (_context.Users.Any(u => u.Email == email))
            {
                TempData["Error"] = "Email already exists.";
                return RedirectToAction("EmployeeDashboard");
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Role = "Farmer"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Message"] = "Farmer added successfully.";
            return RedirectToAction("EmployeeDashboard");
        }

        // --- VIEW FARMER PRODUCTS WITH FILTERING ---
        public IActionResult ViewFarmerProducts(int farmerId, string category, DateTime? startDate, DateTime? endDate)
        {
            if (HttpContext.Session.GetString("UserRole") != "Employee")
                return RedirectToAction("Login", "Account");

            var farmer = _context.Users.FirstOrDefault(u => u.Id == farmerId && u.Role == "Farmer");
            if (farmer == null)
                return NotFound();

            var products = _context.Products
                .Where(p => p.UserId == farmerId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);

            if (startDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate.Value);

            if (endDate.HasValue)
                products = products.Where(p => p.ProductionDate <= endDate.Value);

            ViewBag.FarmerEmail = farmer.Email;
            ViewBag.FarmerId = farmerId;

            return View(products.ToList());
        }
    }
}
