using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using TAG8GJ_HFT_2023241.Endpoint.Services;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerLogic customerLogic;
        IHubContext<SignalRHub> hub;

        public CustomerController(ICustomerLogic customerLogic, IHubContext<SignalRHub> hub)
        {
            this.customerLogic = customerLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Customer> ReadAll()
        {
            return this.customerLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Customer Read(int id)
        {
            return this.customerLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Customer customer)
        {
            this.customerLogic.Create(customer);
            this.hub.Clients.All.SendAsync("CustomerCreated", customer);

        }

        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            this.customerLogic.Update(customer);
            this.hub.Clients.All.SendAsync("CustomerUpdated", customer);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customerToDelete = this.customerLogic.Read(id);
            this.customerLogic.Delete(id);
            this.hub.Clients.All.SendAsync("CustomerDeleted", customerToDelete);

        }

        [HttpGet("SearchByName")]
        public IQueryable<Customer> SearchByName([FromQuery] string name)
        {
            return this.customerLogic.SearchByName(name);
        }
    }
}
