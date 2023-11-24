using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarLogic carLogic;
        public CarController(ICarLogic carLogic)
        {
            this.carLogic = carLogic;
        }

        [HttpGet]
        public IEnumerable<Car> ReadAll()
        {
            return this.carLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Car Read(int id)
        {
            return this.carLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Car carvalue)
        {
            this.carLogic.Create(carvalue);
        }

        [HttpPut]
        public void Put([FromBody] Car carvalue)
        {
            this.carLogic.Update(carvalue);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.carLogic.Delete(id);
        }

        [HttpGet("CarsBelowCertainCost")]
        public IEnumerable<Car> CarsBelowCertainCost([FromQuery] int rentalCost)
        {
            return this.carLogic.CarsBelowCertainCost(rentalCost);
        }
    }
}
