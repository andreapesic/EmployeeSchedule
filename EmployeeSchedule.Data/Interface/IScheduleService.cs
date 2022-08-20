using EmployeeSchedule.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface
{
    public interface IScheduleService : IGenericService<Schedule>
    {
        Task<IEnumerable<Schedule>> GetScheduleForEmployee(int id);
    }
}
