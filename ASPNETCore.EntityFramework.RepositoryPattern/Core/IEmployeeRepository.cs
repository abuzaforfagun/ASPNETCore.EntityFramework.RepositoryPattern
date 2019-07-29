using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Core
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Remove(Employee employee);
        Task<Employee> GetAsync(int id);
        Task<IList<Employee>> GetAsync();
        void Update(Employee employee);
    }
}
