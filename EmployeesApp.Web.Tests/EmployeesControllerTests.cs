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
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void create_test()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
            var controller = new EmployeesController(employeeService.Object);

            var createVM = new CreateVM()
            {
                Name = "Test",
                Email = "test@mail.se",
                BotCheck = 4
            };

            // Act
            var result = controller.Create(createVM);

            // Assert
            employeeService.Verify(x => x.Add(It.Is<Employee>(e =>
            e.Name == createVM.Name &&
            e.Email == createVM.Email
            )), Times.Once);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(Index), redirectResult.ActionName);
            Assert.True(controller.ModelState.IsValid);
        }
    }
}