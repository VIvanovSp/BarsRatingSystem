using Bar_Rating.Data;
using Bar_Rating.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bar_Rating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context=context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/search")]
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Searchd(string text)
        {
            var searchedBars = context.Bars.Where(x => x.Name.Contains(text));
            return View("../Bars/Index", searchedBars);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
