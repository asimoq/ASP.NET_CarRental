using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Endpoint.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerLogic customerLogic;
        public CustomerController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
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
        }

        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            this.customerLogic.Update(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.customerLogic.Delete(id);
        }

        [HttpGet("SearchByName")]
        public IQueryable<Customer> SearchByName([FromQuery] string name)
        {
            return this.customerLogic.SearchByName(name);
        }
    }
}
