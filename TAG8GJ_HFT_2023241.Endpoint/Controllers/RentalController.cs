using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalLogic rentalLogic;

        public RentalController(IRentalLogic rentalLogic)
        {
            this.rentalLogic = rentalLogic;
        }

        [HttpGet]
        public IEnumerable<Rental> ReadAll()
        {
            return this.rentalLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Rental Read(int id)
        {
            return this.rentalLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Rental rental)
        {
            this.rentalLogic.Create(rental);
        }

        [HttpPut]
        public void Put([FromBody] Rental rental)
        {
            this.rentalLogic.Update(rental);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.rentalLogic.Delete(id);
        }

        [HttpGet("{id}/CalculateCost")]
        public decimal CalculateRentalCost(int id)
        {
            return this.rentalLogic.CalculateRentalCost(id);
        }

        [HttpGet("MostFrequentlyRentedCar")]
        public string MostFrequentlyRentedCar()
        {
            return this.rentalLogic.MostFrequentlyRentedCar();
        }

    }
}
