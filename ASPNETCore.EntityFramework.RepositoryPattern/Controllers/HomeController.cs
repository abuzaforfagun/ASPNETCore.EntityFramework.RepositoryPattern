using Microsoft.AspNetCore.Mvc;
using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using ASPNETCore.EntityFramework.RepositoryPattern.ViewModels;

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
            //var emp = new Employee();
            //emp.Name = "Employee 1";
            //emp.Email = "emp@email.com";
            //employeeRepository.Add(emp);
            //unitOfWorkRepository.Complete();
            var viewModel = new IndexViewModel
            {
                Employees = employeeRepository.Get()
            };
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
        
    }
}
