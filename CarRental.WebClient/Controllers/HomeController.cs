using CarRental.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TAG8GJ_HFT_2023241.Client;

namespace CarRental.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RestService rest;

        public HomeController(ILogger<HomeController> logger)
        {
            rest = new RestService("http://localhost:52322/", "car");
            _logger = logger;
        }

        public IActionResult Index()
        {
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