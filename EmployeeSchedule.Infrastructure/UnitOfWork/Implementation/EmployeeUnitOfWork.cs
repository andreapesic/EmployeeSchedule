using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.Repository.Implementation;
using EmployeeSchedule.Repository.Interface;
using System.Threading.Tasks;

namespace EmployeeSchedule.Infrastructure.UnitOfWork.Implementation
{
    public class EmployeeUnitOfWork : IUnitOfWork<Employee>
    {
        public readonly ApplicationDbContext context;
        public EmployeeUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Repository = new EmployeeRepository(context);
        }

        public IRepository<Employee> Repository { get; set; }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
