using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrsServer.Models;

namespace PrsServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PrsServer.Models.User> User { get; set; } = default!;
        public DbSet<PrsServer.Models.Vendor> Vendor { get; set; } = default!;
    }
}
