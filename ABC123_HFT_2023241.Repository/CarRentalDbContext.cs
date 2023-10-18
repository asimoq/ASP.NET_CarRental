using ABC123_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ABC123_HFT_2023241.Repository
{
    internal class CarRentalDbContext : DbContext
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
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\car_rental.mdf;Integrated Security=True";

                builder.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>();

            modelBuilder.Entity<Customer>();

            modelBuilder.Entity<Rental>();
        }


    }
}
