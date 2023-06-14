using Moq;
using NUnit.Framework;
using Sample.Domains;
using Sample.Repositories;
using Sample.Services;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Unit.Sample.Services
{
    [TestFixture]
    public class EmployeeServiceTest
    {
        private Mock<IEmployeeRepository> mockEmployeeRepository;
        private EmployeeService employeeService;

        [SetUp]
        public void Setup()
        {
            mockEmployeeRepository = new Mock<IEmployeeRepository>();
            employeeService = new EmployeeService(mockEmployeeRepository.Object);

        }

        [Test]
        public void Save_WhenEmployeeIdIsZero_ShouldInsertEmployee()
        {           
            // Arrange
            var employee = new Employee { Id = 0 };

            // Act
            employeeService.Save(employee);

            // Assert
            mockEmployeeRepository.Verify(r => r.Insert(employee), Times.Once);
            mockEmployeeRepository.Verify(r => r.Update(It.IsAny<Employee>()), Times.Never);
        }

        [Test]
        public void Save_WhenEmployeeIdIsNotZero_ShouldUpdateEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1 };

            // Act
            employeeService.Save(employee);

            // Assert
            mockEmployeeRepository.Verify(r => r.Insert(It.IsAny<Employee>()), Times.Never);
            mockEmployeeRepository.Verify(r => r.Update(employee), Times.Once);
        }

        [Test]
        public void GetAll_WithFilters_ShouldReturnFilteredEmployees()
        {
            // Arrange
            var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Gender = "Male" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Gender = "Female" },
            new Employee { Id = 3, FirstName = "John", LastName = "Johnson", Gender = "Male" }
        };
            mockEmployeeRepository.Setup(r => r.GetAll()).Returns(employees.AsQueryable());

            // Act
            var result = employeeService.GetAll("John", "Doe", "Male");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("John", result[0].FirstName);
            Assert.AreEqual("Doe", result[0].LastName);
            Assert.AreEqual("Male", result[0].Gender);
        }

        [Test]
        public void GetAll_WithoutFilters_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Gender = "Male" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Gender = "Female" },
            new Employee { Id = 3, FirstName = "John", LastName = "Johnson", Gender = "Male" }
        };
            mockEmployeeRepository.Setup(r => r.GetAll()).Returns(employees.AsQueryable());

            // Act
            var result = employeeService.GetAll(null, null, null);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("John", result[0].FirstName);
            Assert.AreEqual("Doe", result[0].LastName);
            Assert.AreEqual("Male", result[0].Gender);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Jane", result[1].FirstName);
            Assert.AreEqual("Smith", result[1].LastName);
            Assert.AreEqual("Female", result[1].Gender);
            Assert.AreEqual(3, result[2].Id);
            Assert.AreEqual("John", result[2].FirstName);
            Assert.AreEqual("Johnson", result[2].LastName);
            Assert.AreEqual("Male", result[2].Gender);
        }

    }
}
