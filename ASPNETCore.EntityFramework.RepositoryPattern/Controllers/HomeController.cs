using Microsoft.AspNetCore.Mvc;
using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using ASPNETCore.EntityFramework.RepositoryPattern.ViewModels;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Employees = await employeeRepository.GetAsync()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/Form/{id}")]
        public async Task<IActionResult> Form(int id)
        {
            var emp = await employeeRepository.GetAsync(id);
            if (emp == null)
            {
                return View("Error");
            }
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Form(Employee employee)
        {
            if (employee.Id != 0)
            {
                employeeRepository.Update(employee);
            }
            else
            {
                employeeRepository.Add(employee);
            }
            await unitOfWorkRepository.Complete();
            return RedirectToAction("Form", new {id = employee.Id});
        }

        public IActionResult Error()
        {
            return View();
        }
        
    }
}
