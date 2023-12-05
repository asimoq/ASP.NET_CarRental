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

        public string GetCarWithLongestRentalDuration()
        {
            var carIdWithLongestDuration = repo.ReadAll()
                .GroupBy(r => r.CarId)
                .OrderByDescending(g => g.Sum(r => (r.RentalEnd - r.RentalStart).TotalDays))
                .Select(g => g.Key)
                .FirstOrDefault();
            double duration = (repo.Read(carIdWithLongestDuration).RentalEnd - repo.Read(carIdWithLongestDuration).RentalStart).TotalDays;

            if (carIdWithLongestDuration != 0)
            {
                var carWithLongestDuration = repo.ReadAll()
                    .Where(r => r.CarId == carIdWithLongestDuration)
                    .Select(r => r.Car)
                    .FirstOrDefault();

                if (carWithLongestDuration != null)
                {
                    StringBuilder result = new StringBuilder();
                    result.AppendLine($"Car with the longest rental duration:");
                    result.AppendLine($"Car ID: {carWithLongestDuration.CarID}");
                    result.AppendLine($"Brand: {carWithLongestDuration.Brand}");
                    result.AppendLine($"Model: {carWithLongestDuration.Model}");
                    result.AppendLine($"Licence Plate: {carWithLongestDuration.LicencePlate}");
                    result.AppendLine($"Year: {carWithLongestDuration.Year}");
                    result.AppendLine($"Daily Rental Cost: {carWithLongestDuration.DailyRentalCost}");
                    result.AppendLine($"\nDays it was rented for: {duration}");
                    return result.ToString();
                }
            }

            return "No rental records found.";
        }

        public string MostFrequentlyRentedCar()
        {
            var mostFrequentCarId = repo.ReadAll()
                .GroupBy(r => r.CarId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            if (mostFrequentCarId != 0)
            {
                var mostFrequentCar = repo.ReadAll()
                    .Where(r => r.CarId == mostFrequentCarId)
                    .Select(r => r.Car)
                    .FirstOrDefault();

                if (mostFrequentCar != null)
                {
                    StringBuilder result = new StringBuilder();
                    result.AppendLine($"Most frequently rented car details:");
                    result.AppendLine($"Car ID: {mostFrequentCar.CarID}");
                    result.AppendLine($"Brand: {mostFrequentCar.Brand}");
                    result.AppendLine($"Model: {mostFrequentCar.Model}");
                    result.AppendLine($"Licence Plate: {mostFrequentCar.LicencePlate}");
                    result.AppendLine($"Year: {mostFrequentCar.Year}");
                    result.AppendLine($"Daily Rental Cost: {mostFrequentCar.DailyRentalCost}");
                    return result.ToString();
                }
            }

            return "No rental records found.";
        }
    }
}
