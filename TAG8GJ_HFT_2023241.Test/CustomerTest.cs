using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TAG8GJ_HFT_2023241.Logic;
using TAG8GJ_HFT_2023241.Models;
using TAG8GJ_HFT_2023241.Repository;
using System;

namespace TAG8GJ_HFT_2023241.Tests
{
    [TestFixture]
    public class CustomerLogicTests
    {
        [Test]
        public void SearchByName_WithMatchingName_ReturnsMatchingCustomers()
        {
            // Arrange
            var customerRepoMock = new Mock<IRepository<Customer>>();
            var customerLogic = new CustomerLogic(customerRepoMock.Object);

            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "Boncz Márton" },
                new Customer { CustomerId = 2, CustomerName = "Boncz Janka" },
                new Customer { CustomerId = 3, CustomerName = "Kálovics András" }
            };

            customerRepoMock.Setup(repo => repo.ReadAll()).Returns(customers.AsQueryable());

            // Act
            var result = customerLogic.SearchByName("Boncz");

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(c => c.CustomerName.Contains("Boncz")));
        }

        [Test]
        public void SearchByName_WithNoMatchingName_ReturnsEmptyList()
        {
            // Arrange
            var customerRepoMock = new Mock<IRepository<Customer>>();
            var customerLogic = new CustomerLogic(customerRepoMock.Object);

            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "Boncz Márton" },
                new Customer { CustomerId = 2, CustomerName = "Boncz Janka" },
                new Customer { CustomerId = 3, CustomerName = "Kálovics András" }
            };

            customerRepoMock.Setup(repo => repo.ReadAll()).Returns(customers.AsQueryable());

            // Act
            var result = customerLogic.SearchByName("Áron");

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Create_WithValidCustomer()
        {
            // Arrange
            var customerRepoMock = new Mock<IRepository<Customer>>();
            var customerLogic = new CustomerLogic(customerRepoMock.Object);

            var validCustomer = new Customer
            {
                CustomerId = 1,
                CustomerName = "Boncz Márton",
                CustomerEmail = "boncz.marton@example.com",
                CustomerPhone = "123-456-7890"
            };

            // Act
            customerLogic.Create(validCustomer);

            // Assert
            customerRepoMock.Verify(repo => repo.Create(validCustomer), Times.Once);
        }

        [Test]
        public void Create_WithInvalidPhoneNumber_ThrowsArgumentException()
        {
            // Arrange
            var customerRepoMock = new Mock<IRepository<Customer>>();
            var customerLogic = new CustomerLogic(customerRepoMock.Object);

            var invalidPhoneNumberCustomer = new Customer
            {
                CustomerId = 1,
                CustomerName = "Boncz Márton",
                CustomerEmail = "boncz.marton@example.com",
                CustomerPhone = "123-456" //too short
            };

            //Act + Assert
            Assert.Throws<ArgumentException>(() => customerLogic.Create(invalidPhoneNumberCustomer));
            customerRepoMock.Verify(repo => repo.Create(It.IsAny<Customer>()), Times.Never);
        }

        [Test]
        public void Create_WithShortCustomerName_ThrowsArgumentException()
        {
            // Arrange
            var customerRepoMock = new Mock<IRepository<Customer>>();
            var customerLogic = new CustomerLogic(customerRepoMock.Object);

            var shortNameCustomer = new Customer
            {
                CustomerId = 1,
                CustomerName = "BM", //too short
                CustomerEmail = "boncz.marton@example.com",
                CustomerPhone = "123-456-7890"
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => customerLogic.Create(shortNameCustomer));
            customerRepoMock.Verify(repo => repo.Create(It.IsAny<Customer>()), Times.Never);
        }


    }
}
