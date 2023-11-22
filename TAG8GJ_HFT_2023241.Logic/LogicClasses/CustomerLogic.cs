using System;
using System.Linq;
using System.Collections.Generic;
using TAG8GJ_HFT_2023241.Repository;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Logic
{
    public class CustomerLogic : ICustomerLogic
    {
        IRepository<Customer> repo;

        public CustomerLogic(IRepository<Customer> repo)
        {
            this.repo = repo;
        }

        public void Create(Customer entity)
        {
            if (entity.CustomerPhone.Length < 12) throw new ArgumentException("Phone number must be in this format: xxx-xxx-xxxx");
            if (entity.CustomerName.Length < 3) throw new ArgumentException("Customer name must be at least 3 characters long");
            repo.Create(entity);
        }

        public void Delete(int id)
        {

            repo.Delete(id);
        }

        public Customer Read(int id)
        {
            var customer = repo.Read(id);
            if (customer == null) throw new ArgumentException("No customer found with this id");
            return repo.Read(id);
        }

        public IQueryable<Customer> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Customer entity)
        {
            repo.Update(entity);
        }

        //non-CRUD

        public IQueryable<Customer> SearchByName(string name)
        {
            return repo.ReadAll().Where(c => c.CustomerName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
