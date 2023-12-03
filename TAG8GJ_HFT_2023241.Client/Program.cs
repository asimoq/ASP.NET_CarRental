using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TAG8GJ_HFT_2023241.Models;


namespace TAG8GJ_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;

        static void Main(string[] args)
        {
            Console.WriteLine("Program started. Waiting for server connection...");

            rest = new RestService("http://localhost:52322/","car");

            
            var carSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Car"))
               .Add("Create", () => Create("Car"))
               .Add("Delete", () => Delete("Car"))
               .Add("Update", () => Update("Car"))
               .Add("Cars Below Certain Cost", () => CarsBelowCertainCost())
               .Add("Exit", ConsoleMenu.Close);

            var customersSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Customer"))
               .Add("Create", () => Create("Customer"))
               .Add("Delete", () => Delete("Customer"))
               .Add("Update", () => Update("Customer"))
               .Add("Search by Name", () => SearchCustomerByName())
               .Add("Exit", ConsoleMenu.Close);

            var rentalRecordsSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Rental"))
               .Add("Create", () => Create("Rental"))
               .Add("Delete", () => Delete("Rental"))
               .Add("Update", () => Update("Rental"))
               .Add("Calculate Rental Cost", () => CalculateRentalCost())
               .Add("Most Frequently Rented Car", () => MostFrequentlyRentedCar())
               .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                 .Add("Cars", () => carSubMenu.Show())
                 .Add("Customers", () => customersSubMenu.Show())
                 .Add("RentalRecords", () => rentalRecordsSubMenu.Show())
                 .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        private static void MostFrequentlyRentedCar()
        {
            try
            {
                string result = rest.GetSingle<string>("rental/MostFrequentlyRentedCar");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        private static void CalculateRentalCost()
        {
            Console.Write("Enter Rental ID to calculate cost: ");
            if (int.TryParse(Console.ReadLine(), out int rentalId))
            {
                try
                {
                    decimal cost = rest.GetSingle<decimal>($"rental/{rentalId}/CalculateCost");
                    Console.WriteLine($"The rental cost for ID {rentalId} is: ${cost}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Rental ID.");
            }

            Console.ReadLine();
        }

        private static void SearchCustomerByName()
        {
            Console.Write("Enter customer name to search: ");
            string nameToSearch = Console.ReadLine();

            var customers = rest.Get<Customer>($"customer/SearchByName?name={nameToSearch}");

            if (customers.Any())
            {
                Console.WriteLine("Matching Customers:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.CustomerName}, Email: {customer.CustomerEmail}, Phone: {customer.CustomerPhone}");
                }
            }
            else
            {
                Console.WriteLine($"No customers found with the specified name: {nameToSearch}");
            }

            Console.ReadLine();
        }

        private static void CarsBelowCertainCost()
        {
            Console.Write("Enter maximum daily rental cost: ");
            if (int.TryParse(Console.ReadLine(), out int rentalCost))
            { 
                var cars = rest.Get<Car>($"car/CarsBelowCertainCost?rentalCost={rentalCost}");

                if (cars.Any())
                {
                    Console.WriteLine("Cars below this Cost:");
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"Car ID: {car.CarID}, Brand: {car.Brand}, Model: {car.Model}, Daily Rental Cost: {car.DailyRentalCost}");
                    }
                }
                else
                {
                    Console.WriteLine($"No cars below the specified cost of {rentalCost}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for maximum daily rental cost.");
            }

            Console.ReadLine(); 
        }

        private static void Update(string entityType)
        {
            Console.Clear();
            Console.WriteLine($"Update {entityType}");
            Console.Write("Enter ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    switch (entityType)
                    {
                        case "Car":
                            var carToUpdate = rest.GetSingle<Car>($"car/{id}");
                            if (carToUpdate != null)
                            {
                                Console.Write("Enter new Brand: ");
                                carToUpdate.Brand = Console.ReadLine();

                                Console.Write("Enter new Model: ");
                                carToUpdate.Model = Console.ReadLine();

                                rest.Put(carToUpdate, "car");
                                Console.WriteLine("Car updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Car not found!");
                            }
                            break;

                        case "Customer":
                            var customerToUpdate = rest.GetSingle<Customer>($"customer/{id}");
                            if (customerToUpdate != null)
                            {
                                Console.Write("Enter new CustomerName: ");
                                customerToUpdate.CustomerName = Console.ReadLine();

                                Console.Write("Enter new CustomerEmail: ");
                                customerToUpdate.CustomerEmail = Console.ReadLine();

                                rest.Put(customerToUpdate, "customer");
                                Console.WriteLine("Customer updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Customer not found!");
                            }
                            break;

                        case "Rental":
                            var currentRental = rest.GetSingle<Rental>($"rental/{id}");
                            if (currentRental != null)
                            {
                                Console.WriteLine("Current Rental Details:");
                                Console.WriteLine($"Car ID: {currentRental.CarId}");
                                Console.WriteLine($"Customer ID: {currentRental.CustomerId}");
                                Console.WriteLine($"Rental Start Date: {currentRental.RentalStart}");
                                Console.WriteLine($"Rental End Date: {currentRental.RentalEnd}");

                                var updatedRental = new Rental();
                                updatedRental.RentalId = id;

                                Console.Write("Enter new Car ID: ");
                                if (int.TryParse(Console.ReadLine(), out int newCarId))
                                {
                                    updatedRental.CarId = newCarId;
                                }

                                Console.Write("Enter new Customer ID: ");
                                if (int.TryParse(Console.ReadLine(), out int newCustomerId))
                                {
                                    updatedRental.CustomerId = newCustomerId;
                                }

                                Console.Write("Enter new Rental Start Date (yyyy/MM/dd): ");
                                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime newRentalStart))
                                {
                                    updatedRental.RentalStart = newRentalStart;
                                }

                                Console.Write("Enter new Rental End Date (yyyy/MM/dd): ");
                                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime newRentalEnd))
                                {
                                    updatedRental.RentalEnd = newRentalEnd;
                                }

                                rest.Put(updatedRental, "rental");
                                Console.WriteLine("Rental updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine($"Rental with ID {id} not found.");
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid entity type!");
                            break;
                    }
                }catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid ID.");
            }

            Console.ReadLine();
        }


        private static void Delete(string entityType)
        {
            Console.Clear();
            Console.WriteLine($"Delete {entityType}");
            Console.Write("Enter ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                switch (entityType)
                {
                    case "Car":
                        var carToDelete = rest.Get<Car>(id, "car");
                        if (carToDelete != null)
                        {
                            rest.Delete(id, "car");
                            Console.WriteLine("Car deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Car not found!");
                        }
                        break;

                    case "Customer":
                        var customerToDelete = rest.Get<Customer>(id, "customer");
                        if (customerToDelete != null)
                        {
                            rest.Delete(id, "customer");
                            Console.WriteLine("Customer deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Customer not found!");
                        }
                        break;

                    case "Rental":
                        var rentalToDelete = rest.Get<Rental>(id,"rental");
                        if (rentalToDelete != null)
                        {
                            rest.Delete(id, "rental");
                            Console.WriteLine("Rental deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Rental not found!");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid entity type!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid ID.");
            }

            Console.ReadLine(); // Wait for user input
        }

        private static void List(string entityType)
        {
            Console.Clear();
            Console.WriteLine($"List of {entityType}s");

            switch (entityType)
            {
                case "Car":
                    var cars = rest.Get<Car>("car");
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"{car.CarID}. {car.Brand} {car.Model} - {car.DailyRentalCost} per day");
                    }
                    break;

                case "Customer":
                    var customers = rest.Get<Customer>("customer");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.CustomerId}. {customer.CustomerName} ({customer.CustomerEmail}) - {customer.CustomerPhone}");
                    }
                    break;

                case "Rental":
                    var rentals = rest.Get<Rental>("rental");
                    foreach (var rental in rentals)
                    {
                        Console.WriteLine($"{rental.RentalId}. Car: {rental.Car.Brand} {rental.Car.Model}, Customer: {rental.Customer.CustomerName}, Rental Period: {rental.RentalStart.ToShortDateString()} to {rental.RentalEnd.ToShortDateString()}");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid entity type!");
                    break;
            }

            Console.ReadLine(); 
        }

        private static void Create(string entityType)
        {
            Console.Clear();
            Console.WriteLine($"Create {entityType}");
            try
            {
                switch (entityType)
                {
                    case "Car":
                        var newCar = new Car();

                        Console.Write("Enter Brand: ");
                        newCar.Brand = Console.ReadLine();

                        Console.Write("Enter Model: ");
                        newCar.Model = Console.ReadLine();

                        Console.Write("Enter Licence Plate: ");
                        newCar.LicencePlate = Console.ReadLine();

                        Console.Write("Enter Year: ");
                        if (int.TryParse(Console.ReadLine(), out int year))
                        {
                            newCar.Year = year;
                        }

                        Console.Write("Enter Daily Rental Cost: ");
                        if (int.TryParse(Console.ReadLine(), out int dailyRentalCost))
                        {
                            newCar.DailyRentalCost = dailyRentalCost;
                        }

                        rest.Post(newCar, "car");
                        Console.WriteLine("Car created successfully!");
                        break;

                    case "Customer":
                        var newCustomer = new Customer();
                        Console.Write("Enter CustomerName: ");
                        newCustomer.CustomerName = Console.ReadLine();
                        Console.Write("Enter CustomerEmail: ");
                        newCustomer.CustomerEmail = Console.ReadLine();
                        Console.Write("Enter CustomerPhone: ");
                        newCustomer.CustomerPhone = Console.ReadLine();

                        rest.Post(newCustomer, "customer");
                        Console.WriteLine("Customer created successfully!");
                        break;

                    case "Rental":
                        var newRental = new Rental();
                        Console.Write("Enter Car ID: ");
                        if (int.TryParse(Console.ReadLine(), out int carId))
                        {
                            newRental.CarId = carId;
                        }
                        Console.Write("Enter Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out int customerId))
                        {
                            newRental.CustomerId = customerId;
                        }
                        Console.Write("Enter Rental Start Date (yyyy/MM/dd): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime rentalStart))
                        {
                            newRental.RentalStart = rentalStart;
                        }
                        Console.Write("Enter Rental End Date (yyyy/MM/dd): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime rentalEnd))
                        {
                            newRental.RentalEnd = rentalEnd;
                        }

                        rest.Post(newRental, "rental");
                        Console.WriteLine("Rental created successfully!");
                        break;

                    default:
                        Console.WriteLine("Invalid entity type!");
                        break;
                }
            }catch(Exception ex) { Console.WriteLine(ex.Message); }

            Console.ReadLine(); 
        }
    }
}
