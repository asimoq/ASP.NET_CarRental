using Moq;
using NUnit.Framework;
using System;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;
using TAG8GJ_HFT_2023241.Repository;

namespace TAG8GJ_HFT_2023241.Test
{
    [TestFixture]
    public class RentalLogicTests
    {
        
        [Test]
        public void RentalCostTestSameMonth()
        {
            // Arrange
            var rentalRepoMock = new Mock<IRepository<Rental>>();
            var carRepoMock = new Mock<IRepository<Car>>();
            var rentalLogic = new RentalLogic(rentalRepoMock.Object);

            var rentalId = 1;
            var rentalStartDate = new DateTime(2023, 1, 1);
            var rentalEndDate = new DateTime(2023, 1, 5);
            var dailyRentalCost = 50;

            var rental = new Rental
            {
                RentalId = rentalId,
                Car = new Car { DailyRentalCost = dailyRentalCost },
                RentalStart = rentalStartDate,
                RentalEnd = rentalEndDate
            };

            rentalRepoMock.Setup(repo => repo.Read(rentalId)).Returns(rental);

            // Act
            var result = rentalLogic.CalculateRentalCost(rentalId);

            // Assert
            var expectedCost = 250;
            Assert.AreEqual(expectedCost, result);
        }

        [Test]
        public void RentalCostTestDifferentMonth()
        {
            // Arrange
            var rentalRepoMock = new Mock<IRepository<Rental>>();
            var carRepoMock = new Mock<IRepository<Car>>();
            var rentalLogic = new RentalLogic(rentalRepoMock.Object);

            var rentalId = 1;
            var rentalStartDate = new DateTime(2023, 1, 1);
            var rentalEndDate = new DateTime(2023, 2, 5);
            var dailyRentalCost = 50;

            var rental = new Rental
            {
                RentalId = rentalId,
                Car = new Car { DailyRentalCost = dailyRentalCost },
                RentalStart = rentalStartDate,
                RentalEnd = rentalEndDate
            };

            rentalRepoMock.Setup(repo => repo.Read(rentalId)).Returns(rental);

            // Act
            var result = rentalLogic.CalculateRentalCost(rentalId);

            // Assert
            var expectedCost = 1800;
            Assert.AreEqual(expectedCost, result);
        }

        [Test]
        public void Create_ThrowsArgumentExceptionWhenStartDateIsLaterThanEndDate()
        {
            // Arrange
            var rentalRepoMock = new Mock<IRepository<Rental>>();
            var rentalLogic = new RentalLogic(rentalRepoMock.Object);
            var rental = new Rental
            {
                RentalStart = new System.DateTime(2023, 1, 2),
                RentalEnd = new System.DateTime(2023, 1, 1)
            };

            // Act + Assert
            Assert.Throws<System.ArgumentException>(() => rentalLogic.Create(rental));
        }
    }
}

