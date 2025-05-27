using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using Moq;

namespace EmployeeApp.Application.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void GetAll_NoParams_ReturnsAllEmployees()
        {
            // Arrage
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository
                .Setup(o => o.GetAll())
                .Returns([
                    new Employee { Id = 1, Name = "Test Employee 1" },
                    new Employee { Id = 2, Name = "Test Employee 2" }
                ]);

            var service = new EmployeeService(employeeRepository.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Employee[]>(result);
        }

        [Fact]
        public void GetById_ValidId_ReturnsEmployee()
        {
            // Arrage
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository
                .Setup(o => o.GetById(1))
                .Returns(new Employee { Id = 1, Name = "Arne Anka", Email = "arne@anka.com" });

            var service = new EmployeeService(employeeRepository.Object);

            // Act
            var result = service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Employee>(result);
            employeeRepository.Verify(o => o.GetById(1), Times.Exactly(1));
        }


        [Fact]
        public void AddEmployee_NoParams_ShouldAddEmployee()
        {
            // Arrage
            var employeeRepository = new Mock<IEmployeeRepository>();
            var service = new EmployeeService(employeeRepository.Object);

            // Act
            var employee = new Employee
            {
                Name = "john",
                Email = "JOHN.DOE@EXAMPLE.COM"
            };

            service.Add(employee);

            // Assert
            Assert.Equal("John", employee.Name);
            Assert.Equal("john.doe@example.com", employee.Email);
            employeeRepository.Verify(r => r.Add(employee), Times.Once);
        }

        [Fact]
        public void CheckVIP_NoParams_ReturnsResultBool()
        {
            // Arrage
            var employeeRepository = new Mock<IEmployeeRepository>();
            var service = new EmployeeService(employeeRepository.Object);

            // Act
            var employee = new Employee
            {
                Name = "Anders Anka",
                Email = "anders.doe@example.com"
            };

            // Assert
            Assert.True(service.CheckIsVIP(employee));
        }
    }
}
