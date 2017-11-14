using ASPNETCore.EntityFramework.RepositoryPattern.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore.EntityFramework.RepositoryPattern.Presistance
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ApplicationDbContext context;

        public UnitOfWorkRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}
