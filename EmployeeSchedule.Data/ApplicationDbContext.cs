using EmployeeSchedule.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeSchedule.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = 3,
                Adress = "Vladimira Popovica",
                Domain = "Finance",
                IdentificationNumber = "1234",
                Name = "Banca Intesa"
            }
);
        }
    }
}
