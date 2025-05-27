using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Controllers;
using EmployeesApp.Web.Views.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeesApp.Web.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public void Index_NoParams_ReturnsViewResult()
        {
            // Arrage
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(o => o.GetAll())
                .Returns(
                [
                    new Employee { Id = 1, Name = "Oliver" },
                    new Employee { Id = 2, Name = "Pontus" }
                ]);

            //Act
            var controller = new EmployeesController(employeeService.Object);
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]

        public void Details_ValidId_ReturnsViewResultWithEmployee()
        {
            // Arrage
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(o => o.GetById(1))
                .Returns(new Employee { Id = 1, Name = "Oliver" });

            //Act
            var controller = new EmployeesController(employeeService.Object);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(controller.Details(1));
            Assert.NotNull(viewResult.Model);
            

        }

    }
}