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
            var viewModel = new IndexViewModel
            {
                Employees = employeeRepository.Get()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Form(int id)
        {
            var emp = employeeRepository.Get(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Form(Employee employee)
        {
            employeeRepository.Update(employee);
            unitOfWorkRepository.Complete();
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        
    }
}
