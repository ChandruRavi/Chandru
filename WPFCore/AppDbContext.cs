using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCore
{
    class AppDbContext : DbContext
    {
        //Database Configure
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection String 
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Student_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<Student> Students { get; set; }
    }
}
