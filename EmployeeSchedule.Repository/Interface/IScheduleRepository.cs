using EmployeeSchedule.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Repository.Interface
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetScheduleForEmployee(int id);
    }
}
