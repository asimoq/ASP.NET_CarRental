using TAG8GJ_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using static TAG8GJ_HFT_2023241.Models.Car;
using static TAG8GJ_HFT_2023241.Models.Rental;
using static TAG8GJ_HFT_2023241.Models.Customer;
using System.Runtime.ConstrainedExecution;
using System.Windows.Controls;
using Castle.Core.Resource;
using Newtonsoft.Json.Linq;

namespace TAG8GJ_HFT_2023241.WPF_Client
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;
        static RestService r;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        #region Cars

        public RestCollection<Car> Cars { get; set; }
        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (value != null)
                {
                    selectedCar = new Car()
                    {
                        Model = value.Model,
                        CarID = value.CarID,
                        Brand = value.Brand,
                        LicencePlate = value.LicencePlate,
                        Year = value.Year,
                        DailyRentalCost = value.DailyRentalCost
                    };
                    OnPropertyChanged();
                    (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCarCommand { get; set; }
        public ICommand DeleteCarCommand { get; set; }
        public ICommand UpdateCarCommand { get; set; }

        #endregion 
        #region Customers
        public RestCollection<Customer> Customers { get; set; }
        private Customer selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != null)
                {
                    selectedCustomer = new Customer()
                    {
                        CustomerName = value.CustomerName,
                        CustomerId = value.CustomerId,
                        CustomerEmail = value.CustomerEmail,
                        CustomerPhone = value.CustomerPhone,
                    };
                    OnPropertyChanged();
                    (DeleteCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }

        #endregion 
        #region Rental
        public RestCollection<Rental> Rentals { get; set; }
        private Rental selectedRental;

        public Rental SelectedRental
        {
            get { return selectedRental; }
            set
            {
                if (value != null)
                {
                    selectedRental = new Rental()
                    {
                        RentalStart = value.RentalStart,
                        RentalEnd = value.RentalEnd,
                        RentalId = value.RentalId,
                        CarId = value.CarId,
                        CustomerId = value.CustomerId,
                        Car = value.Car,
                        Customer = value.Customer,
                    };
                    OnPropertyChanged();
                    (DeleteRentalCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateRentalCommand { get; set; }
        public ICommand DeleteRentalCommand { get; set; }
        public ICommand UpdateRentalCommand { get; set; }

        #endregion
        #region NONCRUDS
        private int rentalCostInput;

        public int RentalCostInput
        {
            get { return rentalCostInput; }
            set { SetProperty(ref rentalCostInput, value); }
        }

        public ICommand CarsBelowCertainCostCommand { get; set; }
        public ICommand MostFrequentlyRentedCarRentalCommand { get; set; }
        public ICommand GetCarWithLongestRentalDurationRentalCommand { get; set; }

        private ObservableCollection<Car> carsBelowCertainCostResult;
        public ObservableCollection<Car> CarsBelowCertainCostResult
        {
            get { return carsBelowCertainCostResult; }
            set
            {
                carsBelowCertainCostResult = value;
                OnPropertyChanged(nameof(carsBelowCertainCostResult));
            }
        }
        private string mostFrequentlyRentedCarResult;

        public string MostFrequentlyRentedCarResult
        {
            get { return mostFrequentlyRentedCarResult; }
            set
            {
                mostFrequentlyRentedCarResult = value;
                OnPropertyChanged(nameof(MostFrequentlyRentedCarResult));
            }
        }

        private string getCarWithLongestRentalDurationResult;
        

        public string GetCarWithLongestRentalDurationResult
        {
            get { return getCarWithLongestRentalDurationResult; }
            set
            {
                getCarWithLongestRentalDurationResult = value;
                OnPropertyChanged(nameof(GetCarWithLongestRentalDurationResult));
            }
        }


        #endregion




        public MainWindowViewModel()
        {
            r = new RestService("http://localhost:52322/");

            #region Cars
            Cars = new RestCollection<Car>("http://localhost:52322/", "Car", "hub");

            CreateCarCommand = new RelayCommand(() =>
            {
                Cars.Add(new Car()
                {
                    Model = SelectedCar.Model,
                    Brand = SelectedCar.Brand,
                    LicencePlate = SelectedCar.LicencePlate,
                    Year = SelectedCar.Year,
                    DailyRentalCost = SelectedCar.DailyRentalCost
                });
            });

            UpdateCarCommand = new RelayCommand(() =>
            {
                Cars.Update(SelectedCar);
            });

            DeleteCarCommand = new RelayCommand(() =>
            {
                Cars.Delete(SelectedCar.CarID);
            },
            () =>
            {
                return SelectedCar != null;
            });

            SelectedCar = new Car();
            #endregion

            #region Customers
            Customers = new RestCollection<Customer>("http://localhost:52322/", "Customer", "hub");

            CreateCustomerCommand = new RelayCommand(() =>
            {
                Customers.Add(new Customer()
                {
                    CustomerName = SelectedCustomer.CustomerName,
                    CustomerEmail = SelectedCustomer.CustomerEmail,
                    CustomerPhone = SelectedCustomer.CustomerPhone
                });
            });

            UpdateCustomerCommand = new RelayCommand(() =>
            {
                Customers.Update(SelectedCustomer);
            });

            DeleteCustomerCommand = new RelayCommand(() =>
            {
                Customers.Delete(SelectedCustomer.CustomerId);
            },
            () =>
            {
                return SelectedCustomer != null;
            });

            SelectedCustomer = new Customer();
            #endregion

            #region Rentals
            Rentals = new RestCollection<Rental>("http://localhost:52322/", "Rental", "hub");

            CreateRentalCommand = new RelayCommand(() =>
            {
                Rentals.Add(new Rental()
                {
                    RentalStart = SelectedRental.RentalStart,
                    RentalEnd = SelectedRental.RentalEnd,
                    CarId = SelectedCar.CarID,
                    CustomerId = SelectedCustomer.CustomerId,
                    Car = Cars.Where(x => x.CarID == SelectedCar.CarID).FirstOrDefault(),
                    Customer = Customers.Where(x => x.CustomerId == SelectedCustomer.CustomerId).FirstOrDefault(),


                });
            });

            UpdateRentalCommand = new RelayCommand(() =>
            {
                Rentals.Update(SelectedRental);
            });

            DeleteRentalCommand = new RelayCommand(() =>
            {
                Rentals.Delete(SelectedRental.RentalId);
            },
            () =>
            {
                return SelectedRental != null;
            });

            SelectedRental = new Rental();
            #endregion

            // Initialize non-CRUD commands
            CarsBelowCertainCostResult = new ObservableCollection<Car>();


            CarsBelowCertainCostCommand = new RelayCommand(() =>
            {
                try
                {
                    var result = r.Get<IEnumerable<Car>>("Car/CarsBelowCertainCost?rentalCost=" + RentalCostInput);

                    // Handle the result accordingly
                    if (result != null)
                    {
                        // Update the property bound to UI
                        CarsBelowCertainCostResult.Clear();
                        foreach (Car car in result)
                        {
                            CarsBelowCertainCostResult.Add(car);
                        }
                    }
                    else
                    {
                        CarsBelowCertainCostResult.Add(new Car { CarID = 1, Brand = "Toyota", Model = "No Cars Found", LicencePlate = "TAG8GJ", Year = 2020, DailyRentalCost = 50 });
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: {ex.Message}");
                }
            });

            MostFrequentlyRentedCarRentalCommand = new RelayCommand(() =>
            {
                try
                {
                    var result = r.GetSingle<string>("Rental/MostFrequentlyRentedCar");

                    // Handle the result accordingly
                    MostFrequentlyRentedCarResult = result.ToString();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine($"Error: {ex.Message}");
                }
            });

            GetCarWithLongestRentalDurationRentalCommand = new RelayCommand(() =>
            {
                try
                {
                    var result = r.GetSingle<string>("Rental/GetCarWithLongestRentalDuration");

                    // Handle the result accordingly
                    GetCarWithLongestRentalDurationResult = result.ToString();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine($"Error: {ex.Message}");
                }
            });



        }
    }
}
