using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using TAG8GJ_HFT_2023241.Endpoint.Services;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalLogic rentalLogic;
        private readonly IHubContext<SignalRHub> hub;

        public RentalController(IRentalLogic rentalLogic, IHubContext<SignalRHub> hub)
        {
            this.rentalLogic = rentalLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("RentalCreated", rental);

        }

        [HttpPut]
        public void Put([FromBody] Rental rental)
        {
            this.rentalLogic.Update(rental);
            this.hub.Clients.All.SendAsync("RentalUpdated", rental);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var rentalToDelete = this.rentalLogic.Read(id);
            this.rentalLogic.Delete(id);
            this.hub.Clients.All.SendAsync("RentalDeleted", rentalToDelete);

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

        [HttpGet("GetCarWithLongestRentalDuration")]
        public string GetCarWithLongestRentalDuration()
        {
            return this.rentalLogic.GetCarWithLongestRentalDuration();
        }

    }
}
