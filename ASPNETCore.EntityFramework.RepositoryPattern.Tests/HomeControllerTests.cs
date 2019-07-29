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
        private Mock<IEmployeeRepository> _empRepositoryMock;
        private Mock<IUnitOfWorkRepository> _unitOfWorkMock;
        private HomeController _controller;
        public HomeControllerTests()
        {
            _empRepositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWorkRepository>();
            _controller = new HomeController(_unitOfWorkMock.Object, _empRepositoryMock.Object);

            _empRepositoryMock.Setup(u => u.GetAsync()).ReturnsAsync(new List<Employee>
            {
                new Employee{ Id = 1, Email = "abc@a.com", Name = "Abid" }
            });
            _empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(
                new Employee { Id = 1, Email = "abc@a.com", Name = "Abid" }
            );

        }
        [Fact]
        public async void Index_Should_Render_View()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void Index_Should_Contain_Employee_Data()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.IsAssignableFrom<IndexViewModel>(result.Model);

            var viewModel = result.Model as IndexViewModel;
            Assert.Equal(1, (viewModel.Employees.Count));
        }

        [Fact]
        public async void Form_Should_Render_View()
        {
            var result = await _controller.Form(1) as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void Form_Should_Contain_Employee_Data()
        {
            var result = await _controller.Form(1) as ViewResult;
            Assert.IsAssignableFrom<Employee>(result.Model);

            var viewModel = result.Model as Employee;
            Assert.Equal("Abid", viewModel.Name);
            Assert.Equal("abc@a.com", viewModel.Email);
        }

        [Fact]
        public async void Form_Should_ReturnTo_Error_When_EmployeeData_NotFound()
        {
            _empRepositoryMock.Setup(u => u.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Employee));
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), _empRepositoryMock.Object);
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
            var result = _controller.Form(employee);

            _empRepositoryMock.Verify(r => r.Update(employee), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void Post_Form_Should_Create_Record_When_EmployeeIdNotAvailable()
        {
            var employee = new Employee
            {
                Email = "abc@a.com",
                Name = "test1"
            };
            var result = _controller.Form(employee);

            _empRepositoryMock.Verify(r => r.Add(employee), Times.Once);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
    }
}
