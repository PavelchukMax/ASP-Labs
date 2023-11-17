using lab_12_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lab_12_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var companies = _context.Companies.ToList();
            return View(companies);
        }
    }
}