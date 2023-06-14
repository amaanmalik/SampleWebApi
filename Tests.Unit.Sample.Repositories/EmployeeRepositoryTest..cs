using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Sample.Domains;
using Sample.Repositories;

namespace Tests.Unit.Sample.Repositories
{
    [TestFixture]
    public class EmployeeRepositoryTest
    {
        private Mock<EmployeeDbContext> mockContext;
        private EmployeeRepository employeeRepository;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<EmployeeDbContext>();
            employeeRepository = new EmployeeRepository(mockContext.Object);

        }

        [Test]
        public void Delete_ShouldRemoveEmployeeFromContextAndSaveChanges()
        {
            // Arrange
            var employee = new Employee();

            // Act
            employeeRepository.Delete(employee);

            // Assert
            mockContext.Verify(c => c.Employees.Remove(employee), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
