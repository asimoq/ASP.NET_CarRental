using System.Linq;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Logic
{
    public interface ICustomerLogic
    {
        void Create(Customer entity);
        void Delete(int id);
        Customer Read(int id);
        IQueryable<Customer> ReadAll();
        void Update(Customer entity);
        IQueryable<Customer> SearchByName(string name);
    }
}