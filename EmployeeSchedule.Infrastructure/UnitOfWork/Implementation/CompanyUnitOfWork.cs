using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.Repository.Implementation;
using EmployeeSchedule.Repository.Interface;
using System.Threading.Tasks;

namespace EmployeeSchedule.Infrastructure.UnitOfWork.Implementation
{
    public class CompanyUnitOfWork : IUnitOfWork<Company>
    {
        public readonly ApplicationDbContext context;
        public CompanyUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Repository = new CompanyRepository(context);
        }

        public IRepository<Company> Repository { get; set; }

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
