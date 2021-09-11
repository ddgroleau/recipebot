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
    public class DevDbContext : ApplicationDbContext
    {
        public DevDbContext()
        {
        }

        public DevDbContext(DbContextOptions<DevDbContext> options)
            : base(options) { }
    }
}

