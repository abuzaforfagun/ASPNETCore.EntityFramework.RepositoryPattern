using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Core
{
    public interface IUnitOfWorkRepository
    {
        Task Complete();
    }
}
