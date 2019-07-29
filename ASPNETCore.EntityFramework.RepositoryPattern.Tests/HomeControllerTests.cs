using System.Collections.Generic;
using ASPNETCore.EntityFramework.RepositoryPattern.Controllers;
using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using ASPNETCore.EntityFramework.RepositoryPattern.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void Index_Should_Render_View()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.GetAsync()).ReturnsAsync(new List<Employee>());
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = await controller.Index() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void Index_Should_Contain_Employee_Data()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.GetAsync()).ReturnsAsync(new List<Employee>
            {
                new Employee{Email = "abc@a.com", Id = 1, Name = "Abid" }
            });
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = await controller.Index() as ViewResult;
            Assert.IsAssignableFrom<IndexViewModel>(result.Model);

            var viewModel = result.Model as IndexViewModel;
            Assert.Equal(1, (viewModel.Employees.Count));
        }

        [Fact]
        public async void Form_Should_Render_View()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(new Employee());
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = await controller.Form(1) as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void Form_Should_Contain_Employee_Data()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(
                new Employee{Email = "abc@a.com", Id = 1, Name = "Abid" }
            );
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = await controller.Form(1) as ViewResult;
            Assert.IsAssignableFrom<Employee>(result.Model);

            var viewModel = result.Model as Employee;
            Assert.Equal("Abid", viewModel.Name);
            Assert.Equal("abc@a.com", viewModel.Email);
        }

        [Fact]
        public async void Form_Should_ReturnTo_Error_When_EmployeeData_NotFound()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Employee));
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = await controller.Form(1) as ViewResult;

            Assert.Equal("Error", result.ViewName);
            Assert.Null(result.Model);
        }

        [Fact]
        public void Post_Form_Should_Update_Record()
        {
            var employee = new Employee
            {
                Id = 1,
                Email = "abc@a.com",
                Name = "test1"
            };
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWorkRepository>();
            empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(employee);
            var controller = new HomeController(unitOfWorkMock.Object, empRepositoryMock.Object);
            var result = controller.Form(employee);

            empRepositoryMock.Verify(r => r.Update(employee), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void Post_Form_Should_Create_Record_When_EmployeeIdNotAvailable()
        {
            var employee = new Employee
            {
                Email = "abc@a.com",
                Name = "test1"
            };
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWorkRepository>();
            empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(employee);
            var controller = new HomeController(unitOfWorkMock.Object, empRepositoryMock.Object);
            var result = controller.Form(employee);

            empRepositoryMock.Verify(r => r.Add(employee), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
    }
}
