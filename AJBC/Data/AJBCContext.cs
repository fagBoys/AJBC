﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJBC.Models;
using Microsoft.EntityFrameworkCore;


namespace AJBC.Data
{
    public class AJBCContext : DbContext
    {
        public AJBCContext()
        {
        }

        public AJBCContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql11.hostinguk.net; Database=ajbuildi_database; User Id=ajbuildi_dbuser; Password=DBpass123;MultipleActiveResultSets=true");
            //Server =.; Database = CrestDB; Trusted_Connection = True; MultipleActiveResultSets = true
            //Server=sql11.hostinguk.net; Database=crestcou_database; User Id=crestdbuser; Password=CRESTcouriers.db;MultipleActiveResultSets=true
        }
    }
}
