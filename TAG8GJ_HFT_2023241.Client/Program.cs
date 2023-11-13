using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TAG8GJ_HFT_2023241.Models;
using TAG8GJ_HFT_2023241.Repository;

namespace TAG8GJ_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IRepository<Car> repo = new CarRepository(new CarRentalDbContext());

            Car car = new Car()
            {
                Brand = "Tesla",
                Model = "model X",

            };

            repo.Create(car);

            var another = repo.Read(1);
            another.Model = "Sajtostangli2000";
            repo.Update(another);

            var items = repo.ReadAll().ToArray();

            ;
        }
    }
}
