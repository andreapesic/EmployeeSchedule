using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.Repository.Implementation;
using EmployeeSchedule.Repository.Interface;
using System.Threading.Tasks;

namespace EmployeeSchedule.Infrastructure.UnitOfWork.Implementation
{
    public class ScheduleUnitOfWork : IUnitOfWork<Schedule>
    {
        public readonly ApplicationDbContext context;
        public ScheduleUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Repository = new ScheduleRepository(context);
        }

        public IRepository<Schedule> Repository { get; set; }

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