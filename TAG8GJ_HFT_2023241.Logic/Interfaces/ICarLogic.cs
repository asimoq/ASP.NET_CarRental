using System.Collections.Generic;
using System.Linq;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Logic
{
    public interface ICarLogic
    {
        IEnumerable<Car> CarsBelowCertainCost(int rentalCost);
        void Create(Car entity);
        void Delete(int id);
        Car Read(int id);
        IQueryable<Car> ReadAll();
        void Update(Car entity);
    }
}