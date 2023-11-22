using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Repository;
using TAG8GJ_HFT_2023241.Models;
using System;

namespace TAG8GJ_HFT_2023241.Tests
{
    [TestFixture]
    public class CarLogicTests
    {
        [Test]
        public void CarsBelowCertainCost_ReturnsCorrectCars()
        {
            // Arrange
            var carRepoMock = new Mock<IRepository<Car>>();
            var carLogic = new CarLogic(carRepoMock.Object);

            var cars = new List<Car>
            {
                new Car { CarID = 1, Brand = "Brand1", DailyRentalCost = 50 },
                new Car { CarID = 2, Brand = "Brand2", DailyRentalCost = 75 },
                new Car { CarID = 3, Brand = "Brand3", DailyRentalCost = 60 }
            };

            carRepoMock.Setup(repo => repo.ReadAll()).Returns(cars.AsQueryable);

            // Act
            var result = carLogic.CarsBelowCertainCost(70);

            // Assert
            Assert.AreEqual(2, result.Count());  
            Assert.IsTrue(result.Any(car => car.CarID == 1));  
            Assert.IsTrue(result.Any(car => car.CarID == 3));  
        }

        [Test]
        public void Create_WithZeroDailyRentalCost_ThrowsArgumentException()
        {
            // Arrange
            var carRepoMock = new Mock<IRepository<Car>>();
            var carLogic = new CarLogic(carRepoMock.Object);
            var carWithZeroCost = new Car
            {
                CarID = 1,
                Brand = "Mazda",
                DailyRentalCost = 0,
                Year = DateTime.Today.Year - 1
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => carLogic.Create(carWithZeroCost));
            carRepoMock.Verify(repo => repo.Create(It.IsAny<Car>()), Times.Never);
        }

        [Test]
        public void Create_WithFutureYear_ThrowsArgumentException()
        {
            // Arrange
            var carRepoMock = new Mock<IRepository<Car>>();
            var carLogic = new CarLogic(carRepoMock.Object);
            var carWithFutureYear = new Car
            {
                CarID = 1,
                Brand = "Mazda",
                DailyRentalCost = 50,
                Year = DateTime.Today.Year + 1
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => carLogic.Create(carWithFutureYear));
            carRepoMock.Verify(repo => repo.Create(It.IsAny<Car>()), Times.Never);
        }

        [Test]
        public void Create_WithValidData()
        {
            // Arrange
            var carRepoMock = new Mock<IRepository<Car>>();
            var carLogic = new CarLogic(carRepoMock.Object);
            var validCar = new Car
            {
                CarID = 1,
                Brand = "Mazda",
                DailyRentalCost = 50,
                Year = DateTime.Today.Year - 1
            };

            // Act
            carLogic.Create(validCar);

            // Assert
            carRepoMock.Verify(repo => repo.Create(validCar), Times.Once);
        }
    }
}