using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.Repository.Implementation;
using EmployeeSchedule.Repository.Interface;
using System.Threading.Tasks;

namespace EmployeeSchedule.Infrastructure.UnitOfWork.Implementation
{
    public class CompanyDomainUnitOfWork : IUnitOfWork<CompanyDomain>
    {
        public readonly ApplicationDbContext context;
        public CompanyDomainUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Repository = new CompanyDomainRepository(context);
        }

        public IRepository<CompanyDomain> Repository { get; set; }

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
