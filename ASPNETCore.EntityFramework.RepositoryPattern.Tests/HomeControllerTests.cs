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
        public void Index_Should_Render_View()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.Get()).Returns(new List<Employee>());
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = controller.Index() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_Should_Contain_Employee_Data()
        {
            var empRepositoryMock = new Mock<IEmployeeRepository>();
            empRepositoryMock.Setup(u => u.Get()).Returns(new List<Employee>
            {
                new Employee{Email = "abc@a.com", Id = 1, Name = "Abid" }
            });
            var controller = new HomeController(It.IsAny<IUnitOfWorkRepository>(), empRepositoryMock.Object);
            var result = controller.Index() as ViewResult;
            Assert.IsAssignableFrom<IndexViewModel>(result.Model);

            var viewModel = result.Model as IndexViewModel;
            Assert.Equal(1, (viewModel.Employees.Count));
        }
    }
}
