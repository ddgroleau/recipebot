using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Text;

namespace PBC.Shared.Common.Data
{
    public class StagingDbContext : ApplicationDbContext
    {
        public StagingDbContext()
        {
        }

        public StagingDbContext(DbContextOptions<StagingDbContext> options)
            : base(options) { }

    }
}

