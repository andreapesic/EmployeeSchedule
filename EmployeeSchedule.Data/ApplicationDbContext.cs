using EmployeeSchedule.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
