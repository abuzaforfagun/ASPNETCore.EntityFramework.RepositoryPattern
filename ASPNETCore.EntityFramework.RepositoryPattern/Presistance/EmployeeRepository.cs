using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using System.Collections.Generic;
using System.Linq;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Presistance
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await context.Employees.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IList<Employee>> GetAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public void Remove(Employee employee)
        {
            context.Employees.Remove(employee);
        }

        public void Update(Employee employee)
        {
            var emp = context.Employees.Single(e => e.Id == employee.Id);
            emp.Name = employee.Name;
            emp.Email = employee.Email;
        }
    }
}
