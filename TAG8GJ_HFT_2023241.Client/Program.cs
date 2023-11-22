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
        private static readonly CarRentalDbContext ctx = new CarRentalDbContext();

        private static readonly IRepository<Car> carRepo = new CarRepository(ctx);
        private static readonly IRepository<Customer> customerRepo = new CustomerRepository(ctx);
        private static readonly IRepository<Rental> rentalRepo = new RentalRepository(ctx);

        private static readonly ICarLogic carLogic = new CarLogic(carRepo);
        private static readonly ICustomerLogic customerLogic = new CustomerLogic(customerRepo);
        private static readonly IRentalLogic rentalLogic = new RentalLogic(rentalRepo);

        static void Main(string[] args)
        {  
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
               .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                 .Add("Cars", () => carSubMenu.Show())
                 .Add("Customers", () => customersSubMenu.Show())
                 .Add("RentalRecords", () => rentalRecordsSubMenu.Show())
                 .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        private static void CalculateRentalCost()
        {
            Console.Write("Enter Rental ID to calculate cost: ");
            if (int.TryParse(Console.ReadLine(), out int rentalId))
            {
                try
                {
                    decimal cost = rentalLogic.CalculateRentalCost(rentalId);
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

            var customers = customerLogic.SearchByName(nameToSearch);

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
            if (int.TryParse(Console.ReadLine(), out int maxCost))
            {
                var cars = carLogic.CarsBelowCertainCost(maxCost);

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
                    Console.WriteLine($"No cars below the specified cost of {maxCost}.");
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
                switch (entityType)
                {
                    case "Car":
                        var carToUpdate = carLogic.Read(id);
                        if (carToUpdate != null)
                        {
                            Console.Write("Enter new Brand: ");
                            carToUpdate.Brand = Console.ReadLine();

                            Console.Write("Enter new Model: ");
                            carToUpdate.Model = Console.ReadLine();

                            
                            carLogic.Update(carToUpdate);
                            Console.WriteLine("Car updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Car not found!");
                        }
                        break;

                    case "Customer":
                        var customerToUpdate = customerLogic.Read(id);
                        if (customerToUpdate != null)
                        {
                            Console.Write("Enter new CustomerName: ");
                            customerToUpdate.CustomerName = Console.ReadLine();

                            Console.Write("Enter new CustomerEmail: ");
                            customerToUpdate.CustomerEmail = Console.ReadLine();

                            
                            customerLogic.Update(customerToUpdate);
                            Console.WriteLine("Customer updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Customer not found!");
                        }
                        break;

                    case "Rental":
                        Console.Write("Enter Rental ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int rentalId))
                        {
                            
                            var currentRental = rentalLogic.Read(rentalId);
                            if (currentRental != null)
                            {
                                Console.WriteLine("Current Rental Details:");
                                Console.WriteLine($"Car ID: {currentRental.CarId}");
                                Console.WriteLine($"Customer ID: {currentRental.CustomerId}");
                                Console.WriteLine($"Rental Start Date: {currentRental.RentalStart}");
                                Console.WriteLine($"Rental End Date: {currentRental.RentalEnd}");

                                
                                var updatedRental = new Rental();
                                updatedRental.RentalId = rentalId;

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

                                
                                rentalLogic.Update(updatedRental);
                                Console.WriteLine("Rental updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine($"Rental with ID {rentalId} not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Rental ID format.");
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
                        var carToDelete = carLogic.Read(id);
                        if (carToDelete != null)
                        {
                            carLogic.Delete(id);
                            Console.WriteLine("Car deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Car not found!");
                        }
                        break;

                    case "Customer":
                        var customerToDelete = customerLogic.Read(id);
                        if (customerToDelete != null)
                        {
                            customerLogic.Delete(id);
                            Console.WriteLine("Customer deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Customer not found!");
                        }
                        break;

                    case "Rental":
                        var rentalToDelete = rentalLogic.Read(id);
                        if (rentalToDelete != null)
                        {
                            rentalLogic.Delete(id);
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
                    var cars = carLogic.ReadAll();
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"{car.CarID}. {car.Brand} {car.Model} - {car.DailyRentalCost} per day");
                    }
                    break;

                case "Customer":
                    var customers = customerLogic.ReadAll();
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.CustomerId}. {customer.CustomerName} ({customer.CustomerEmail}) - {customer.CustomerPhone}");
                    }
                    break;

                case "Rental":
                    var rentals = rentalLogic.ReadAll();
                    foreach (var rental in rentals)
                    {
                        Console.WriteLine($"{rental.RentalId}. Car: {rental.Car.Brand} {rental.Car.Model}, Customer: {rental.Customer.CustomerName}, Rental Period: {rental.RentalStart.ToShortDateString()} to {rental.RentalEnd.ToShortDateString()}");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid entity type!");
                    break;
            }

            Console.ReadLine(); // Wait for user input
        }

        private static void Create(string entityType)
        {
            Console.Clear();
            Console.WriteLine($"Create {entityType}");

            switch (entityType)
            {
                case "Car":
                    var newCar = new Car();
                    Console.Write("Enter Brand: ");
                    newCar.Brand = Console.ReadLine();
                    Console.Write("Enter Model: ");
                    newCar.Model = Console.ReadLine();
                    Console.Write("Enter Daily Rental Cost: ");
                    if (int.TryParse(Console.ReadLine(), out int dailyRentalCost))
                    {
                        newCar.DailyRentalCost = dailyRentalCost;
                    }
                    // Add other properties as needed
                    carLogic.Create(newCar);
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
                    // Add other properties as needed
                    customerLogic.Create(newCustomer);
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
                    // Add other properties as needed
                    rentalLogic.Create(newRental);
                    Console.WriteLine("Rental created successfully!");
                    break;

                default:
                    Console.WriteLine("Invalid entity type!");
                    break;
            }

            Console.ReadLine(); // Wait for user input
        }
    }
}
