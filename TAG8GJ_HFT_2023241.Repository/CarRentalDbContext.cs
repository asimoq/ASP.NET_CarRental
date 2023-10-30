using TAG8GJ_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TAG8GJ_HFT_2023241.Repository
{
    public class CarRentalDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }


        public CarRentalDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if(!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\car_rental.mdf;Integrated Security=True;MultipleActiveResultsSets = true";

                builder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>();

            modelBuilder.Entity<Customer>();

            

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            // Cars tábla kezdeti adatok
            modelBuilder.Entity<Car>().HasData(
                new Car { CarID = 1, Brand = "Toyota", Model = "Camry", LicencePlate = "TAG8GJ", Year = 2020, DailyRentalCost = 50 },
                new Car { CarID = 2, Brand = "Honda", Model = "Civic", LicencePlate = "XYZ789", Year = 2019, DailyRentalCost = 45 },
                new Car { CarID = 3, Brand = "Ford", Model = "Focus", LicencePlate = "EFG456", Year = 2022, DailyRentalCost = 55 },
                new Car { CarID = 4, Brand = "Chevrolet", Model = "Malibu", LicencePlate = "LMN789", Year = 2021, DailyRentalCost = 48 },
                new Car { CarID = 5, Brand = "Nissan", Model = "Altima", LicencePlate = "OPQ123", Year = 2019, DailyRentalCost = 47 },
                new Car { CarID = 6, Brand = "Hyundai", Model = "Elantra", LicencePlate = "RST456", Year = 2020, DailyRentalCost = 49 },
                new Car { CarID = 7, Brand = "Kia", Model = "Forte", LicencePlate = "UVW789", Year = 2021, DailyRentalCost = 51 },
                new Car { CarID = 8, Brand = "Mazda", Model = "Mazda3", LicencePlate = "XYZ123", Year = 2018, DailyRentalCost = 46 },
                new Car { CarID = 9, Brand = "Subaru", Model = "Impreza", LicencePlate = "ABC456", Year = 2020, DailyRentalCost = 52 },
                new Car { CarID = 10, Brand = "Volkswagen", Model = "Jetta", LicencePlate = "DEF789", Year = 2021, DailyRentalCost = 53 }
            );

            // Customers tábla kezdeti adatok
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "John Doe", CustomerEmail = "john.doe@example.com", CustomerPhone = "123-456-7890" },
                new Customer { CustomerId = 2, CustomerName = "Jane Smith", CustomerEmail = "jane.smith@example.com", CustomerPhone = "987-654-3210" },
                new Customer { CustomerId = 3, CustomerName = "Bob Johnson", CustomerEmail = "bob.johnson@example.com", CustomerPhone = "555-555-5555" },
                new Customer { CustomerId = 4, CustomerName = "Alice Brown", CustomerEmail = "alice.brown@example.com", CustomerPhone = "111-222-3333" },
                new Customer { CustomerId = 5, CustomerName = "David Wilson", CustomerEmail = "david.wilson@example.com", CustomerPhone = "444-777-8888" },
                new Customer { CustomerId = 6, CustomerName = "Emily White", CustomerEmail = "emily.white@example.com", CustomerPhone = "999-333-6666" },
                new Customer { CustomerId = 7, CustomerName = "Kevin Davis", CustomerEmail = "kevin.davis@example.com", CustomerPhone = "123-123-1234" },
                new Customer { CustomerId = 8, CustomerName = "Laura Lee", CustomerEmail = "laura.lee@example.com", CustomerPhone = "555-123-7890" },
                new Customer { CustomerId = 9, CustomerName = "Michael Moore", CustomerEmail = "michael.moore@example.com", CustomerPhone = "444-444-1234" },
                new Customer { CustomerId = 10, CustomerName = "Sarah Hall", CustomerEmail = "sarah.hall@example.com", CustomerPhone = "222-888-1111" }
            );

            // Rentals tábla kezdeti adatok
            modelBuilder.Entity<Rental>().HasData(
                new Rental { RentalId = 1, CarId = 1, CustomerId = 1, RentalStart = new DateTime(2023, 1, 1), RentalEnd = new DateTime(2023, 1, 5) },
                new Rental { RentalId = 2, CarId = 2, CustomerId = 2, RentalStart = new DateTime(2023, 2, 10), RentalEnd = new DateTime(2023, 2, 15) },
                new Rental { RentalId = 3, CarId = 3, CustomerId = 3, RentalStart = new DateTime(2023, 3, 20), RentalEnd = new DateTime(2023, 3, 25) },
                new Rental { RentalId = 4, CarId = 4, CustomerId = 4, RentalStart = new DateTime(2023, 4, 2), RentalEnd = new DateTime(2023, 4, 6) },
                new Rental { RentalId = 5, CarId = 5, CustomerId = 5, RentalStart = new DateTime(2023, 5, 15), RentalEnd = new DateTime(2023, 5, 20) },
                new Rental { RentalId = 6, CarId = 6, CustomerId = 6, RentalStart = new DateTime(2023, 6, 10), RentalEnd = new DateTime(2023, 6, 15) },
                new Rental { RentalId = 7, CarId = 7, CustomerId = 7, RentalStart = new DateTime(2023, 7, 5), RentalEnd = new DateTime(2023, 7, 10) },
                new Rental { RentalId = 8, CarId = 8, CustomerId = 8, RentalStart = new DateTime(2023, 8, 18), RentalEnd = new DateTime(2023, 8, 22) },
                new Rental { RentalId = 9, CarId = 9, CustomerId = 9, RentalStart = new DateTime(2023, 9, 12), RentalEnd = new DateTime(2023, 9, 18) },
                new Rental { RentalId = 10, CarId = 10, CustomerId = 10, RentalStart = new DateTime(2023, 10, 3), RentalEnd = new DateTime(2023, 10, 8) }
            );


            
        }


    }
}
