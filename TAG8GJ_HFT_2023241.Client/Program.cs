using ConsoleTools;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;
using TAG8GJ_HFT_2023241.Repository;

namespace TAG8GJ_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CarRentalDbContext();

            var carRepo = new CarRepository(ctx);
            var customerRepo = new CustomerRepository(ctx);
            var rentalRepo = new RentalRepository(ctx);

            var carLogic = new CarLogic(carRepo);
            var customerLogic = new CustomerLogic(customerRepo);
            var rentalLogic = new RentalLogic(rentalRepo);

            
            
            var carSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Car"))
               .Add("Create", () => Create("Car"))
               .Add("Delete", () => Delete("Car"))
               .Add("Update", () => Update("Car"))
               .Add("Exit", ConsoleMenu.Close);

            var customersSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Customer"))
               .Add("Create", () => Create("Customer"))
               .Add("Delete", () => Delete("Customer"))
               .Add("Update", () => Update("Customer"))
               .Add("Exit", ConsoleMenu.Close);

            var rentalRecordsSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Rental"))
               .Add("Create", () => Create("Rental"))
               .Add("Delete", () => Delete("Rental"))
               .Add("Update", () => Update("Rental"))
               .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                 .Add("Cars", () => carSubMenu.Show())
                 .Add("Customers", () => customersSubMenu.Show())
                 .Add("RentalRecords", () => rentalRecordsSubMenu.Show())
                 .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }

        private static void Update(string v)
        {
            throw new NotImplementedException();
        }

        private static void Delete(string v)
        {
            throw new NotImplementedException();
        }

        private static void List(string v)
        {
            throw new NotImplementedException();
        }

        private static void Create(string v)
        {
            throw new NotImplementedException();
        }
    }
}
