﻿using System;
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

        public DbSet<PrsServer.Models.User> Users { get; set; } = default!;
        public DbSet<PrsServer.Models.Vendor> Vendors { get; set; } = default!;
        public DbSet<PrsServer.Models.Product> Products { get; set; } = default!;
        public DbSet<PrsServer.Models.Request> Requests { get; set; } = default!;
        public DbSet<PrsServer.Models.RequestLine> RequestLines { get; set; } = default!;
        public DbSet<PrsServer.Models.PO> POs { get; set; }
        public DbSet<PrsServer.Models.POline> POLines { get; set; }

    }
}
