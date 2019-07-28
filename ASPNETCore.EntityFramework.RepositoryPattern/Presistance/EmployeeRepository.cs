using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using System.Collections.Generic;
using System.Linq;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;

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

        public Employee Get(int id)
        {
            return context.Employees.SingleOrDefault(e => e.Id == id);
        }

        public IList<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public void Remove(Employee employee)
        {
            context.Employees.Remove(employee);
        }
    }
}
