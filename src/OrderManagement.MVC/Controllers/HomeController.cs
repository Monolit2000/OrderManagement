using Microsoft.AspNetCore.Mvc;
using OrderManagement.MVC.Models;
using System.Diagnostics;

namespace OrderManagement.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }


        public IActionResult Product()
        {
            return View();
        }

        public IActionResult CreateProduct()
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
