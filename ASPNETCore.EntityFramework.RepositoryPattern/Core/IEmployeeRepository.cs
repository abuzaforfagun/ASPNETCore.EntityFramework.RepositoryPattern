using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using System.Collections.Generic;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Core
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Remove(Employee employee);
        Employee Get(int id);
        IList<Employee> Get();
        void Update(Employee employee);
    }
}
