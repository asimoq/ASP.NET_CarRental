using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(CarRentalDbContext ctx) : base(ctx) { }

        public override Customer Read(int id)
        {
            return ctx.Customers.FirstOrDefault(t =>t.CustomerId ==id);
        }

        public override void Update(Customer customer)
        {
            var existingCustomer = Read(customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.CustomerEmail = customer.CustomerEmail;
                existingCustomer.CustomerPhone = customer.CustomerPhone;

                ctx.SaveChanges();
            }
        }
    }
}
