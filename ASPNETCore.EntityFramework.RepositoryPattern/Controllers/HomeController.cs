using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore.EntityFramework.RepositoryPattern.ViewModels;
using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;
        private readonly IEmployeeRepository employeeRepository;

        public HomeController(IUnitOfWorkRepository unitOfWorkRepository, IEmployeeRepository employeeRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
            this.employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            var emp = new Employee();
            emp.Name = "Employee 1";
            emp.Email = "emp@email.com";
            employeeRepository.Add(emp);
            unitOfWorkRepository.Complete();
            return View();
        }
    }
}
