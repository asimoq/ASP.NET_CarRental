using System;
using System.Collections.Generic;
using System.Linq;
using TAG8GJ_HFT_2023241.Logic.Interfaces;
using TAG8GJ_HFT_2023241.Models.ModelClasses;
using TAG8GJ_HFT_2023241.Repository;

namespace TAG8GJ_HFT_2023241
{
    public class CarLogic : ICarLogic
    {
        IRepository<Car> repo;

        public CarLogic(IRepository<Car> repo)
        {
            this.repo = repo;
        }

        public void Create(Car entity)
        {
            if (entity.DailyRentalCost <= 0) throw new ArgumentException("The daily rental cost must be above zero");
            if (entity.Year > DateTime.Today.Year) throw new ArgumentException("The model year cant be larger than the current year");
            this.repo.Create(entity);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Car Read(int id)
        {
            var car = this.repo.Read(id);
            if (car == null) throw new ArgumentException("No car found with this id");
            return this.repo.Read(id);
        }

        public IQueryable<Car> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Car entity)
        {
            this.repo.Update(entity);
        }

        //non CRUD

        public IEnumerable<Car> CarsBelowCertainCost(int rentalCost)
        {
            return this.repo
                .ReadAll()
                .Where(t => t.DailyRentalCost < rentalCost);
        }
    }
}
