using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PBC.Shared.Common.Data;

namespace PBC.Shared.Common
{
    public class DbFactory : IFactory<ApplicationDbContext>
    {
        private readonly ApplicationDbContext _dbContext;

        public DbFactory(IHostingEnvironment env)
        {
            _dbContext = new ApplicationDbContext();
        }


        public ApplicationDbContext Make()
        {
            ApplicationDbContext dbContext = _dbContext;
            return dbContext;
        }
    }
}
