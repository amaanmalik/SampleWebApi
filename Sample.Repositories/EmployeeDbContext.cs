using Microsoft.EntityFrameworkCore;
using Sample.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Sample.Repositories
{
    public class EmployeeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
    }
}
