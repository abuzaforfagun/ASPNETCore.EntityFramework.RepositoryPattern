using System.Collections.Generic;
using ASPNETCore.EntityFramework.RepositoryPattern.Core.Models;

namespace ASPNETCore.EntityFramework.RepositoryPattern.ViewModels
{
    public class IndexViewModel
    {
        public IList<Employee> Employees { get; set; }
    }
}
