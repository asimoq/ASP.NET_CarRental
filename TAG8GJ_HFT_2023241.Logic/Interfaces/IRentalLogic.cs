﻿using System.Linq;
using TAG8GJ_HFT_2023241.Models;

namespace TAG8GJ_HFT_2023241.Logic
{
    public interface IRentalLogic
    {
        void Create(Rental entity);
        void Delete(int id);
        Rental Read(int id);
        IQueryable<Rental> ReadAll();
        void Update(Rental entity);
        decimal CalculateRentalCost(int rentalId);
        string MostFrequentlyRentedCar();
        string GetCarWithLongestRentalDuration();
    }
}