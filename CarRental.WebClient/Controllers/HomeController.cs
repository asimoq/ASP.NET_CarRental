using CarRental.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using TAG8GJ_HFT_2023241.Logic;

namespace CarRental.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICarLogic carLogic;
        private IRentalLogic rentalLogic;
        private ICustomerLogic customerLogic;

        public HomeController(ILogger<HomeController> logger, ICarLogic carLogic, IRentalLogic rentalLogic, ICustomerLogic customerLogic)
        {
            _logger = logger;
            this.carLogic = carLogic;
            this.rentalLogic = rentalLogic;
            this.customerLogic = customerLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
		public IActionResult CreateCar()
		{
			return View();
		}

		public IActionResult ListCars()
        {
            var list = this.carLogic.ReadAll().Distinct().ToList();

			return View(list);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}