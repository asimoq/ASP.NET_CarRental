using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAG8GJ_HFT_2023241.Models.ModelClasses;

namespace TAG8GJ_HFT_2023241.Repository
{
    public class CarRepository : Repository<Car>
    {
        public CarRepository(CarRentalDbContext ctx) : base(ctx) { }

        public override Car Read(int id)
        {
            return ctx.Cars.FirstOrDefault(t => t.CarID == id);
        }

        public override void Update(Car car)
        {
            var existingCar = Read(car.CarID);
            if (existingCar != null)
            {
                existingCar.Brand = car.Brand;
                existingCar.Model = car.Model;
                existingCar.LicencePlate = car.LicencePlate;
                existingCar.Year = car.Year;
                existingCar.DailyRentalCost = car.DailyRentalCost;

                ctx.SaveChanges();
            }
        }
    }
}
