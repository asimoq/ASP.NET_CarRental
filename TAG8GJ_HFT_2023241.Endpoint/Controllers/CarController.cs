using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TAG8GJ_HFT_2023241.Endpoint.Services;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarLogic carLogic;
        IHubContext<SignalRHub> hub;

        public CarController(ICarLogic carLogic, IHubContext<SignalRHub> hub)
        {
            this.carLogic = carLogic;
            this.hub = hub;
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
        public void Create([FromBody] Car value)
        {
            this.carLogic.Create(value);
            this.hub.Clients.All.SendAsync("CarCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Car value)
        {
            this.carLogic.Update(value);
            this.hub.Clients.All.SendAsync("CarUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var carToDelete = this.carLogic.Read(id);
            this.carLogic.Delete(id);
            this.hub.Clients.All.SendAsync("CarDeleted", carToDelete);
        }

        [HttpGet("CarsBelowCertainCost")]
        public IEnumerable<Car> CarsBelowCertainCost([FromQuery] int rentalCost)
        {
            return this.carLogic.CarsBelowCertainCost(rentalCost);
        }
    }
}
