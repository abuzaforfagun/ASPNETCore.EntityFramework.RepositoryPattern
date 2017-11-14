using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Core
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Remove(Employee employee);
        Employee Get(int id);
    }
}
