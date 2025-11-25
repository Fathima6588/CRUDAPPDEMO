using System.Diagnostics;
using CRUDAPPDEMO.Models;
using CRUDAPPDEMO.DATA;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPPDEMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // provide simple dashboard counts used by the view
            ViewBag.StudentCount = _context.Students?.Count() ?? 0;
            ViewBag.TeacherCount = _context.Teachers?.Count() ?? 0;
            // If you have a Courses DbSet, add ViewBag.CourseCount similarly
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
