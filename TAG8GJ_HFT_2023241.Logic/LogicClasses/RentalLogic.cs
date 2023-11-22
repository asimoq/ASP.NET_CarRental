using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;
using TAG8GJ_HFT_2023241.Repository;

namespace TAG8GJ_HFT_2023241.Logic
{
    public class RentalLogic : IRentalLogic
    {
        IRepository<Rental> repo;

        public RentalLogic(IRepository<Rental> repo)
        {
            this.repo = repo;
        }

        public void Create(Rental entity)
        {
            if (entity.RentalStart > entity.RentalEnd)
            {
                throw new ArgumentException("Start date cannot be later than end date");
            }

            repo.Create(entity);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Rental Read(int id)
        {
            var rental = repo.Read(id);
            if (rental == null) throw new ArgumentException("No Rental Record found with this id");
            return repo.Read(id);
        }

        public IQueryable<Rental> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Rental entity)
        {
            repo.Update(entity);
        }

        //non CRUD

        public decimal CalculateRentalCost(int rentalId)
    {
        var rental = repo.Read(rentalId);

        if (rental == null)
        {
            throw new ArgumentException($"No rental found with ID: {rentalId}");
        }

        int rentalDuration = (int)(rental.RentalEnd - rental.RentalStart).TotalDays + 1;
        decimal dailyRentalCost = rental.Car.DailyRentalCost;

        return rentalDuration * dailyRentalCost;
    }
    }
}
